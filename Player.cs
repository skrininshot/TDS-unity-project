using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
   
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float diggingTime = 5f;
    private Rigidbody2D rigidbody;
    private ItemChecker checker;
    private Interactive interactingItem;
    private bool isInteracting { get {return interactingItem != null; } }

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactive nearest = checker.GetNearestInteractive();
            if (nearest is null) return;
            if (isInteracting) return;
            interactingItem = nearest;

            switch (nearest)
            {
                case Item:
                    InteractItem(nearest);
                    break;
                case Huckster:
                    nearest.Interact();
                    interactingItem = null;
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
        if (FindObjectOfType<Inventory>().HasSpace)
        {
            StopMoving();
            LookAt(interactiveItem.transform.position);
            yield return new WaitForSeconds(diggingTime);
            interactiveItem.Interact();
        }
        interactingItem = null;
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