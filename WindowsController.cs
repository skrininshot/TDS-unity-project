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
        FindObjectOfType<Player>().Freeze = visible;
        Debug.Log(menu.GetType().ToString() + " " + (visible ? "show" : "hide"));
    }

    public void ShowInventory(bool visible)
    {
        ShowMenu(windows[0], visible);
    }

    public void ShowShop(bool visible)
    {
        ShowMenu(windows[1], visible);
    }

    public void ShowFinalScreen(bool visible)
    {
        ShowMenu(windows[2], visible);
    }

    public void ShowPause(bool visible)
    {
        ShowMenu(windows[3], visible);
    }

    public void ShowMainMenu(bool visible)
    {
        ShowMenu(windows[4], visible);
    }

    public void HideAll()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].Accessible = false;
        }
    }
}
