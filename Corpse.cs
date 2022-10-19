using System.Collections;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    private float speed = 25f;
    private void Start()
    {
        StartCoroutine(FallingAnimation());
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.15f);
    }
    IEnumerator FallingAnimation()
    {
        while (speed > 0.01f)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            transform.position += transform.right * speed * Time.deltaTime;
            speed -= speed / 1.1f;
        }
        Destroy(GetComponent<Corpse>());

    }
}