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
    private SpriteRenderer sprite;
    private Color color = Color.white;
    private float alpha = 0f;

    private Player player;
    private void Start()
    {
        moveSpeed = 2.5f;
        bullet = bulletPrefab;
        player = FindObjectOfType<Player>();
        StartCoroutine(ShootingInPlayer());
        StartCoroutine(ChangeAlpha());
    }

    private IEnumerator ChangeAlpha()
    {
        sprite = GetComponent<SpriteRenderer>();
        while (alpha < 1)
        {
            alpha += 0.1f;
            color.a = alpha;
            sprite.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            StopMoving();
            return;
        }
        LookAt(player.transform.position);

        Vector3 direction = player.transform.position - transform.position;

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
        Quaternion scatter = transform.rotation;
        scatter.z += Random.Range(-0.1f, 0.1f);
        newBullet.transform.rotation = scatter;

        newBullet.Damage = damage;
    }

    private IEnumerator ShootingInPlayer()
    {
        yield return new WaitForSeconds(2f);
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
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        spawner.StartTimer();
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
    }
}