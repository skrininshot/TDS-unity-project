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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var Interactive = other.GetComponent<Interactive>();
        if (Interactive != null)
        {
            Interactives.Remove(Interactive);
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