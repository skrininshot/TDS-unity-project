using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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
    protected int maxHP = 100;
    public int Health
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
    protected int health = 100;
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
    protected int damage;
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
    protected float shootingSpeed = 50f;

    protected Bullet bullet;

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

    public virtual void Movement()
    {

    }

    public virtual void Die()
    {

    }
}
