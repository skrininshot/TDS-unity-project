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

    
}
