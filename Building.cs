using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Building : MonoBehaviour
{
    private Collider2D col;
    private EnemySpawner spawner;
    [SerializeField] private Doors[] doors;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        spawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player is not null)
        {
            spawner.StartTimer();
        }
    }

    public void Close()
    {
        col.isTrigger = false;
        foreach (Doors door in doors)
        {
            door.Close();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player is not null && FindObjectsOfType<Enemy>().Length == 0)
        {
            col.isTrigger = true;
            spawner.StopTimer();
            foreach (Doors door in doors)
            {
                door.Open();
            }
        }
    }
}
