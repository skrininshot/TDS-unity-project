using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huckster : Interactive
{
    public override void Interact()
    {
        FindObjectOfType<WindowsController>().ShowShop(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<ItemChecker>();
        if (player != null)
        {
            FindObjectOfType<WindowsController>().windows[1].Accessible = true;
            Debug.Log("Enter to huckster");
        }  
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<ItemChecker>();
        if (player != null)
            FindObjectOfType<WindowsController>().windows[1].Accessible = false;
        Debug.Log("Exit huckster");
    }
}