using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    private SpriteRenderer sprite;
    private float alpha = 0;
    private Color color;
    private Rigidbody2D rb;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        color = sprite.color;
        rb.velocity = transform.right * speed;
        StartCoroutine(BulletAnimation());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.Log("Bullet destroyed");
    }

    private IEnumerator BulletAnimation()
    {
        while (alpha < 1)
        {
            transform.localScale = Vector3.right * (alpha * 2) + Vector3.up;
            alpha += 0.25f;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        alpha = 1;
        while (alpha > 0)
        {
            color.a = alpha;
            sprite.color = color;
            alpha -= 0.25f;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Destroy(gameObject);
    }
}
