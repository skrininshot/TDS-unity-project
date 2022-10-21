using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Menu
{
    public enum ShopOptionTypes { MaxHP, MaxDamage, LootCost, LootTime, MoveSpeed, MaxAmmo, ShootSpeed, Luck, InventorySlot }

    private List<ShopOption> Options = new List<ShopOption>();
    private ShopOption selectedOption = null;
    
    [SerializeField] TextControl headerText;
    [SerializeField] TextControl descriptionText;
    [SerializeField] TextControl costText;

    [SerializeField] private Button sellButton;
    private Player player;
    public override bool Accessible
    {
        get => base.Accessible;
        set 
        {
            base.Accessible = value;
            sellButton.gameObject.SetActive(value);
        }
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        accessible = false;
        sellButton.gameObject.SetActive(false);
        menuButton.SetActive(false);
        ClearInfo();
        Options.AddRange(FindObjectsOfType<ShopOption>());
        Debug.Log("Start count of cells in shop: " + Options.Count);
    }

    public void SetSelectedState(ShopOption selectedOption, bool isSelected)
    {
        foreach (ShopOption option in Options)
        {
            if (option != selectedOption)
            option.IsSelected = false;
        }
        this.selectedOption = (selectedOption.IsSelected ? selectedOption : null);
        SetOptionInfo();
    }

    private void SetOptionInfo()
    {
        if (selectedOption == null)
        {
            ClearInfo();
            return;
        }
        headerText.Text = selectedOption.OptionName + $" : {selectedOption.OptionLevel} Lvl";
        descriptionText.Text = selectedOption.OptionDescription;
        costText.Text = selectedOption.OptionCost + "$";
    }

    public void Buy()
    {
        if (selectedOption == null) return;
        if (player.Money < selectedOption.OptionCost) return;
        player.Money -= selectedOption.OptionCost;
        selectedOption.OptionLevel++;
         
        ShopOptionTypes optionType = selectedOption.OptionType;
        switch (optionType)
        {
            case ShopOptionTypes.MaxHP:
                player.MaxHP += (int)selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.MaxDamage:
                player.Damage += (int)selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.LootCost:
                player.LootCost += selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.LootTime:
                player.LootTime -= selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.MoveSpeed:
                player.MoveSpeed += selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.MaxAmmo:
                player.MaxAmmo += (int)selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.ShootSpeed:
                player.ShootingSpeed -= selectedOption.OptionPoint;
                break;
            case ShopOptionTypes.Luck:
                break;
            case ShopOptionTypes.InventorySlot:
                FindObjectOfType<Inventory>().AddCell();
                break;
        }
        SetOptionInfo();
    }

    private void ClearInfo()
    {
        headerText.Clear();
        descriptionText.Clear();
        costText.Clear();
    }
}