using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
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

    [SerializeField] private Bullet bulletPrefab;

    private Camera cam;
    [SerializeField] private float camSpeed = 100f;

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
        if (isInteracting) return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Movement(new Vector3(moveX, moveY, 0).normalized);
        LookAt(GetPointerInWorld(), 0.5f);  
        
    }

    private void Update()
    {
        Interact();
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
            yield return new WaitForSeconds(diggingTime);
            interactiveItem.Interact();
        }

        isInteracting = false;
    }

    public override void Shooting()
    {
        if (isInteracting) return;
        base.Shooting();
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