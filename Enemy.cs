using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    public override float MoveSpeed
    {
        get => base.MoveSpeed;
        set
        {
            base.MoveSpeed = value;
            moveSpeed = value;
        }
    }

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

    public override void Shooting()
    {
        Bullet newBullet = Instantiate(bullet).GetComponent<Bullet>();
        newBullet.transform.position = transform.position + transform.right * 1.25f;
        float scatter = Random.Range(-2, 2);
        newBullet.transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,transform.rotation.z + scatter);
        newBullet.Damage = damage;
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
        if (player is not null) player.Score += 100;
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