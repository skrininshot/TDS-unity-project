using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject corpseObject;
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
        GameObject corpse = Instantiate(corpseObject);
        corpse.transform.rotation = damageDirection;
        corpse.transform.position = transform.position;
        Destroy(gameObject);
    }
}
