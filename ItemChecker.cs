using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    [HideInInspector] public List<Interactive> Interactives;
    private void Start()
    {
        Interactives = new List<Interactive>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var Interactive = other.GetComponent<Interactive>();
        if (Interactive != null)
        {
            Interactives.Add(Interactive);
            Debug.Log("Interactive enter the checker. Interactives count: " + Interactives.Count.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var Interactive = other.GetComponent<Interactive>();
        if (Interactive != null)
        {
            Interactives.Remove(Interactive);
            Debug.Log("Interactive exit the checker. Interactives count: " + Interactives.Count);
        }
    }

    public Interactive GetNearestInteractive()
    {
        if (Interactives.Count == 0) return null;

        float minDistance = Mathf.Infinity;
        Interactive nearest = null;
        foreach (Interactive potentialNearest in Interactives)
        {
            float currentDistance = (potentialNearest.transform.position - transform.position).sqrMagnitude;
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearest = potentialNearest;
            }
        }
        return nearest;
    }
}
