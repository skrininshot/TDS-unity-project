using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    public Menu[] windows;
    public void ShowMenu(Menu menu, bool visible)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetVisible(false);
        }
        menu.SetVisible(visible);
        FindObjectOfType<Player>().IsInteracting = visible;
    }

    public void ShowInventory(bool visible)
    {
        ShowMenu(windows[0], visible);
    }

    public void ShowShop(bool visible)
    {
        ShowMenu(windows[1], visible);
    }

    public void HideAll()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].Accessible = false;
        }
    }
}
