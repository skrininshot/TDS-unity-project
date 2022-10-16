using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject corpseObject;
    [SerializeField] private Item itemPrefab;

    private Player player;
    private void Start()
    {
        moveSpeed = 2.5f;
        bullet = bulletPrefab;
        player = FindObjectOfType<Player>();
        StartCoroutine(ShootingInPlayer());
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;
        LookAt(player.transform.position);

        Vector3 direction = player.transform.position - transform.position;

        if (player == null)
        {
            StopMoving();
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 5f)
        {
            Movement(new Vector3(direction.x, direction.y, 0).normalized);
        }
        else if (rb.velocity.sqrMagnitude > 0)
        {
            StopMoving();
        }
    }

    private IEnumerator ShootingInPlayer()
    {
        yield return new WaitForSeconds(1f);
        do
        {
            yield return new WaitForSeconds(0.5f);
            Shooting();
        }
        while (player != null);
    }

    public override void Die()
    {
        Corpse corpse = Instantiate(corpseObject).GetComponent<Corpse>();
        corpse.transform.rotation = damageDirection;
        corpse.transform.position = transform.position;
        SpawnItems();
        Destroy(gameObject);    
    }

    private void SpawnItems()
    {
        int[] countProbability = { 1, 1, 1, 1, 2, 2, 2, 3, 3 };
        int randomCount = Random.Range(1, countProbability.Length);

        for (int i = 0; i < countProbability[randomCount]; i++)
        {
            Item newItem = Instantiate(itemPrefab, FindObjectOfType<ItemsManager>().transform).GetComponent<Item>();
            Vector3 randomPosition = Quaternion.Euler(0, 0, Random.Range(-180, 180)) * transform.right * (Random.Range(0, 20) / 10f);
            Vector3 playerPosition = transform.position + randomPosition;
            newItem.transform.position = new Vector3(playerPosition.x, playerPosition.y, -0.2f);
        }
        Debug.Log($"Spawned {countProbability[randomCount]} items");
    }
}
