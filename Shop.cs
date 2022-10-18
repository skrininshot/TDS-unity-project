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
    private int[] optionsLevels = new int[9];
    private float[] optionsPoints = new float[9]; 

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

        optionsNames[0] = "Max HP";
        optionsPoints[0] = 10;
        optionsInfo[0] = $"This option will increase your maximum health. \nAdds {(int)optionsPoints[0]} to max health";
        optionsCosts[0] = 50;

        optionsNames[1] = "Damage";
        optionsCosts[1] = 100;
        optionsPoints[1] = 5;
        optionsInfo[1] = $"This option will increase the maximum damage dealt. \nAdds {(int)optionsPoints[1]} to damage";
        
        optionsNames[2] = "Loot Cost";
        optionsCosts[2] = 200;
        optionsPoints[2] = 0.25f;
        optionsInfo[2] = $"This option will increase the price you can sell items for. \nMakes items more expensive by {optionsPoints[2]} times"; 

        optionsNames[3] = "Loot Time"; 
        optionsCosts[3] = 500;
        optionsPoints[3] = 1f;
        optionsInfo[3] = $"This option will reduce the speed of collecting items. \nReduces loot time by {optionsPoints[3]} seconds";

        optionsNames[4] = "Movement Speed";
        optionsCosts[4] = 200;
        optionsPoints[4] = 1f;
        optionsInfo[4] = $"This option will increase your movement speed. \nAdds {(int)optionsPoints[4]} to your speed";

        optionsNames[5] = "Max Ammo";
        optionsCosts[5] = 200;
        optionsPoints[5] = 5f;
        optionsInfo[5] = $"This option will increase the maximum number of ammo in a clip. \nAdds {(int)optionsPoints[5]} bullets to your max ammo";
        
        optionsNames[6] = "Shoot speed";
        optionsCosts[6] = 300;
        optionsPoints[5] = 0.1f;
        optionsInfo[6] = $"This option will increase your weapon's rate of fire. \nReduces shoot speed by {optionsPoints[6]} seconds";
        
        optionsNames[7] = "Luck";
        optionsCosts[7] = 1000;
        optionsPoints[7] = 0; //XD
        optionsInfo[7] = $"This option will increase your your luck. But is it worth relying on it?";
        
        optionsNames[8] = "Inventory slot";
        optionsCosts[8] = 500;
        optionsPoints[8] = 1;
        optionsInfo[8] = $"This option gives you one extra slot in your inventory. \nAdds {(int)optionsPoints[8]} inventory slot to your inventory";

        ClearInfo();
        Options.AddRange(FindObjectsOfType<ShopOption>());
        Debug.Log("Start count of cells in shop: " + Options.Count);

        for(int i = 0; i < optionsLevels.Length; i++)
        {
            optionsLevels[i] = 1;
        }
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
        int index = (int)selectedOption.OptionType;
        headerText.Text = optionsNames[index].ToString() + $" : {optionsLevels[index]} Lvl";
        descriptionText.Text = optionsInfo[index];
        costText.Text = (optionsCosts[index] * optionsCosts[index]).ToString() + "$";
    }

    public void Buy()
    {
        if (selectedOption == null) return;
        int index = (int)selectedOption.OptionType;
        int cost = optionsCosts[index] * optionsLevels[index];
        if (player.Money < cost) return;
        player.Money -= cost;
        optionsLevels[index]++;
         
        ShopOptionTypes optionType = selectedOption.OptionType;
        switch (optionType)
        {
            case ShopOptionTypes.MaxHP:
                player.MaxHP += (int)optionsPoints[index];
                break;
            case ShopOptionTypes.MaxDamage:
                player.Damage = (int)optionsPoints[index];
                break;
            case ShopOptionTypes.LootCost:
                player.LootCost += optionsPoints[index];
                break;
            case ShopOptionTypes.LootTime:
                player.LootTime -= optionsPoints[index];
                break;
            case ShopOptionTypes.MoveSpeed:
                player.MoveSpeed += optionsPoints[index];
                break;
            case ShopOptionTypes.MaxAmmo:
                player.MaxAmmo += (int)optionsPoints[index];
                break;
            case ShopOptionTypes.ShootSpeed:
                player.ShootingSpeed -= optionsPoints[index];
                break;
            case ShopOptionTypes.Luck:
                //Nothing --_--
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
