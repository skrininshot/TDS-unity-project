using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Menu
{
    public enum ShopOptionTypes { MaxHP, MaxDamage, LootCost, LootTime, MoveSpeed, MaxAmmo, ShootSpeed, Luck, InventorySlot }
    private string[] optionsNames = new string[9];
    private string[] optionsInfo = new string[9];
    private int[] optionsCosts = new int[9];

    private List<ShopOption> Options = new List<ShopOption>();
    private ShopOption selectedOption = null;
    
    [SerializeField] TextControl headerText;
    [SerializeField] TextControl descriptionText;
    [SerializeField] TextControl costText;

    [SerializeField] private Button sellButton;

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
        accessible = false;

        optionsNames[0] = "Max HP";
        optionsInfo[0] = "This option will increase your maximum health";
        optionsCosts[0] = 50;

        optionsNames[1] = "Max Damage";
        optionsInfo[1] = "This option will increase the maximum damage dealt";
        optionsCosts[1] = 50;

        optionsNames[2] = "Loot Cost";
        optionsInfo[2] = "This option will increase the price you can sell items for";
        optionsCosts[2] = 50;

        optionsNames[3] = "Loot Time";
        optionsInfo[3] = "This option will reduce the speed of collecting items";
        optionsCosts[3] = 50;

        optionsNames[4] = "Movement Speed";
        optionsInfo[4] = "This option will increase your movement speed";
        optionsCosts[4] = 50;

        optionsNames[5] = "Max Ammo";
        optionsInfo[5] = "This option will increase the maximum number of ammo in a clip";
        optionsCosts[5] = 50;

        optionsNames[6] = "Shoot speed";
        optionsInfo[6] = "This option will increase your weapon's rate of fire";
        optionsCosts[6] = 50;

        optionsNames[7] = "Luck";
        optionsInfo[7] = "This option will increase your your luck. But is it worth relying on it?";
        optionsCosts[7] = 50;

        optionsNames[8] = "Inventory slot";
        optionsInfo[8] = "This option gives you one extra slot in your inventory.";
        optionsCosts[8] = 50;

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

        headerText.Text = optionsNames[(int)selectedOption.OptionType];
        descriptionText.Text = optionsInfo[(int)selectedOption.OptionType];
        costText.Text = optionsCosts[(int)selectedOption.OptionType].ToString() + "$";
    }

    public void Buy()
    {
        if (selectedOption == null) return;
        ShopOptionTypes optionType = selectedOption.OptionType;
        switch (optionType)
        {
            case ShopOptionTypes.MaxHP:
                break;
        }
    }

    private void ClearInfo()
    {
        headerText.Clear();
        descriptionText.Clear();
        costText.Clear();
    }
}
