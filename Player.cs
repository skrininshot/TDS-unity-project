using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
   
    public float MoveSpeed
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
    [SerializeField] private float moveSpeed = 5f;

    public float DiggingTime
    {
        get
        {
            return diggingTime;
        }
        set
        {
            diggingTime = value;
        }
    }
    [SerializeField] private float diggingTime = 5f;

    public int MaxHP
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
    [SerializeField] private int maxHP = 100;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    [SerializeField] private int health = 100;

    public float ShootingSpeed
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
    [SerializeField] private float shootingSpeed = 50f;

    public int MaxAmmo
    {
        get
        {
            return maxAmmo;
        }
        set
        {
            maxAmmo = value;
        }
    }
    [SerializeField] private int maxAmmo = 20;

    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }
    [SerializeField] private int money = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            Score = value;
        }
    }
    [SerializeField] private int score;

    [SerializeField] private Bullet bullet;

    private Rigidbody2D rigidbody;
    private ItemChecker checker;
    public bool IsInteracting
    {
        get
        {
            return isInteracting;
        }
        set
        {
            isInteracting = value;
            if (value) StopMoving();
        }
    }
    private bool isInteracting;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        checker = transform.GetComponentInChildren<ItemChecker>();
    }

    void FixedUpdate()
    {
        if (isInteracting) return;
        Movement();
        LookAt(GetPointerInWorld(), 0.5f);  
        
    }

    private void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (isInteracting) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactive nearest = checker.GetNearestInteractive();
            if (nearest is null) return;
            StopMoving();
            switch (nearest)
            {
                case Item:
                    InteractItem(nearest);
                    break;
                case Huckster:
                    nearest.Interact();
                    break;
            }    
        }
    }

    private void InteractItem(Interactive item)
    {
        StartCoroutine(Digging(item));
    }

    private IEnumerator Digging(Interactive interactiveItem)
    {
        isInteracting = true;

        if (FindObjectOfType<Inventory>().HasSpace)
        {
            LookAt(interactiveItem.transform.position);
            yield return new WaitForSeconds(diggingTime);
            interactiveItem.Interact();
        }

        isInteracting = false;
    }

    private void StopMoving()
    {
        rigidbody.velocity = new Vector2(0, 0);
    }

    private void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveX, moveY, 0).normalized;
        rigidbody.velocity = direction * moveSpeed;
    }

    public void Shooting()
    {
        if (isInteracting) return;
        Bullet newBullet = Instantiate(bullet).GetComponent<Bullet>();
        newBullet.transform.position = transform.position + transform.right * 1.5f;
        newBullet.transform.rotation = transform.rotation;
    }

    private Vector3 GetPointerInWorld()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void LookAt(Vector3 point, float speed = 1f)
    {
        Vector3 lookPosition = point - transform.position;
        float angle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), speed);
    }
}