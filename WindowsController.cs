using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    public Menu[] windows;

    public void ShowMenu(int menu, bool visible)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetVisible(false);
        }
        windows[menu].SetVisible(visible);
    }

    public void ShowInventory(bool visible)
    {
        ShowMenu(0, visible);
    }

    public void ShowShop(bool visible)
    {
        ShowMenu(1, visible);
    }
}
