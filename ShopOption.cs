using UnityEngine;
using UnityEngine.UI;

public class ShopOption : MonoBehaviour
{
    public Shop.ShopOptionTypes OptionType;
    public string OptionName;
    public string OptionDescription;
    public int OptionCost;
    public float OptionPoint;
    public int OptionLevel
    {
        get
        {
            return optionLevel;
        }
        set
        {
            optionLevel = value;
            OptionCost *= optionLevel;
        }
    }
    private int optionLevel;
    [SerializeField] private Color selectedColor;
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