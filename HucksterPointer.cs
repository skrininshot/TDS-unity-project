using UnityEngine;

public class HucksterPointer : MonoBehaviour
{
    private Vector3 huckster;
    private void Start()
    {
        huckster = FindObjectOfType<Building>().transform.position;
    }
    void Update()
    {
        Vector3 lookPosition = huckster - transform.position;
        float angle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
