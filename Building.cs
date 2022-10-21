using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Building : MonoBehaviour
{
    private CircleCollider2D col;
    private EnemySpawner spawner;
    [SerializeField] private Doors[] doors;
    [SerializeField] private HucksterPointer pointer;
    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        spawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player is not null)
        {
            spawner.StartTimer();
            pointer.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() is not null)
        {
            pointer.gameObject.SetActive(false);
            return;
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

    public void Open()
    {
        col.isTrigger = true;
        foreach (Doors door in doors)
        {
            door.Open();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player is null || FindObjectsOfType<Enemy>().Length > 0) return;
        Open();
        spawner.StopTimer();
    }
}
