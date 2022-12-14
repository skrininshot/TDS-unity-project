using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public virtual int MaxHP
    {
        get
        {
            return maxHP;
        }
        set
        {
            maxHP = value;
        }
    }
    [SerializeField] protected int maxHP = 100;
    public virtual int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }
    [SerializeField] protected int health = 100;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    [SerializeField] protected int damage = 25;
    public virtual float ShootingSpeed
    {
        get
        {
            return shootingSpeed;
        }
        set
        {
            shootingSpeed = value;
        }
    }
    [SerializeField] protected float shootingSpeed = 0.5f;

    public virtual float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }
    [SerializeField] protected float moveSpeed = 10f;

    protected Bullet bullet;
    protected Rigidbody2D rb;

    [HideInInspector] public Quaternion damageDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Shooting()
    {
        Bullet newBullet = Instantiate(bullet).GetComponent<Bullet>();
        newBullet.transform.position = transform.position + transform.right * 1.25f;
        newBullet.transform.rotation = transform.rotation;
        newBullet.Damage = damage;
    }

    public virtual void LookAt(Vector3 point, float speed = 1f)
    {
        Vector3 lookPosition = point - transform.position;
        float angle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), speed);
    }

    public virtual void Movement(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    public virtual void  StopMoving()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public virtual void Die()
    {
        StopMoving();
    }
}