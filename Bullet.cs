using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private SpriteRenderer sprite;
    private float alpha = 0;
    private Color color;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
        color.a = alpha;
        sprite.color = color;
        StartCoroutine(DestroyTimer());
        StartCoroutine(ChangeAlpha());
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private Rigidbody2D rb;
    private void Update()
    {
        //transform.position += transform.right * speed * Time.deltaTime;
        //rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.Log("Bullet destroyed");
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator ChangeAlpha()
    {
        while (alpha < 1)
        {
            color.a = alpha;
            sprite.color = color;
            alpha += 0.25f;
            yield return new WaitForEndOfFrame();
        }    
    }
}
