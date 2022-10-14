using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    private Transform target;

    void Start()
    {
        target = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Vector3 targetPoint = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }
}
