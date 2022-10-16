using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Bullet bulletPrefab;
    private void Start()
    {
        bullet = bulletPrefab;
    }
}
