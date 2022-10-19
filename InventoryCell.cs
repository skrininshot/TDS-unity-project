using UnityEngine;
using UnityEngine.UI;
public class InventoryCell : MonoBehaviour
{
    public InventoryItem Item
    {
        get
        {
            return item;
        }
        set
        {
            SetItem(value);
        }
    }
    [SerializeField] private Color selectedColor;
    private Color defaultColor;
    private InventoryItem item = null;

    private void Start()
    {
        defaultColor = GetComponent<Image>().color;
    }

    private void SetItem(InventoryItem value)
    {
        item = value;
        string newName = (value == null ? "" : $" ({value.name})");
        name = $"Inventory cell" + newName;
    }

    public void SetSelectedState(bool state)
    {
        Image image = GetComponent<Image>();
        image.color = (state ? selectedColor : defaultColor);
    }
}