using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    public float LootTime
    {
        get
        {
            return lootTime;
        }
        set
        {
            lootTime = value;
            if (lootTime < 0)
            {
                lootTime = 0;
            }
        }
    }
    [SerializeField] private float lootTime = 5f;
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
            moneyText.Text = money.ToString() + "$";
        }
    }
    [SerializeField] private int money = 0;
    public float LootCost
    {
        get => lootCost;
        set => lootCost = value;
    }
    [SerializeField] private float lootCost = 1;
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
    public int Ammo { get => ammo; set => ammo = value; }
    [SerializeField] private int ammo;
    public int Bullets { get => bullets; set => bullets = value; }
    [SerializeField] private int bullets;
    public override int Health
    {
        get => base.Health;
        set 
        {
            base.Health = value;
        }
    }
    public override int MaxHP { 
        get => base.MaxHP;
        set
        {
            base.MaxHP = value;
        }
    }

    public override float ShootingSpeed { 
        get => base.ShootingSpeed;
        set
        {
            base.ShootingSpeed = value;
            if (shootingSpeed < 0.1f)
            {
                shootingSpeed = 0.1f;
            }
        }
    }

    public bool Freeze
    {
        get => freeze;
        set
        {
            freeze = value;
        }
    }
    private bool freeze;

    private Camera cam;
    [SerializeField] private float camSpeed = 100f;

    [SerializeField] private Bullet bulletPrefab;

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
    public bool HoldTrigger
    {
        get => holdTrigger;
        set => holdTrigger = value;
    }
    private bool holdTrigger;
    private bool canShoot = true;

    [SerializeField] private GameObject corpseObject;
    [SerializeField] private TextControl moneyText;
      
    private void Start()
    {
        moveSpeed = 5f;
        cam = Camera.main;
        bullet = bulletPrefab;
        rb = GetComponent<Rigidbody2D>();
        checker = transform.GetComponentInChildren<ItemChecker>();
    }

    void FixedUpdate()
    {
        if (freeze) return;
        if (isInteracting) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Movement(new Vector3(moveX, moveY, 0).normalized);
        LookAt(GetPointerInWorld(), 0.5f);  
        
    }
    private void Update()
    {
        if (freeze) return;
        Interact();
        if (holdTrigger) Shooting();
    }

    void LateUpdate()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, transform.position + Vector3.forward * -50f, camSpeed * Time.deltaTime);
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
            yield return new WaitForSeconds(lootTime);
            interactiveItem.Interact();
        }

        isInteracting = false;
    }

    public override void Shooting()
    {
        if (!canShoot || isInteracting) return;
        base.Shooting();
        canShoot = false;
        StartCoroutine(ShootTimer());
    }

    private IEnumerator ShootTimer()
    {  
        yield return new WaitForSeconds(shootingSpeed);
        canShoot = true;
    }

    private Vector3 GetPointerInWorld()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public override void Die()
    {
        base.Die();
        FindObjectOfType<WindowsController>().HideAll();
        Corpse corpse = Instantiate(corpseObject).GetComponent<Corpse>();
        corpse.transform.rotation = damageDirection;
        corpse.transform.position = transform.position;
        Destroy(gameObject);
    }
}