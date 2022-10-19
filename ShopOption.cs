using UnityEngine;
using UnityEngine.UI;

public class ShopOption : MonoBehaviour
{   
    public Shop.ShopOptionTypes OptionType; 
    [HideInInspector] public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            SetSelectedState(value);
        }
    }
    private bool isSelected;
    [SerializeField] private Color selectedColor;
    private Color defaultColor;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    public void OnButton()
    {
        SetSelectedState(!isSelected);
        FindObjectOfType<Shop>().SetSelectedState(this, isSelected);
    }

    private void SetSelectedState(bool value)
    {
        isSelected = value;
        image.color = (isSelected ? selectedColor : defaultColor);
    }
}