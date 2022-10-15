using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] protected GameObject menuButton;
    [SerializeField] protected GameObject exitZone;
    public bool Accessible
    {
        get
        {
            return accessible;
        }
        set
        {
            if (!value) FindObjectOfType<WindowsController>().ShowMenu(this, false);
            menuButton.SetActive(value);
            accessible = value;
        }
    }
    protected bool accessible = true;
    public virtual void SetVisible(bool visible)
    {
        if (!accessible) return;
        menuButton.SetActive(!visible);
        exitZone.SetActive(visible);
        RectTransform rt = transform.GetChild(1).GetComponent<RectTransform>();
        if (visible) { rt.localPosition = new Vector2(0, 0); }
        else { rt.localPosition = Vector3.left * 750; }
    }
}
