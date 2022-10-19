using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Menu
{
    [HideInInspector] public InventoryItem selectedItem = null;
    public List<InventoryItem> Items = new List<InventoryItem>();
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemsParent;
    public List<InventoryCell> Cells = new List<InventoryCell>();
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform cellsParent;
    [SerializeField] private GameObject dropItemPrefab;
    [SerializeField] private TextControl itemName;
    [SerializeField] private TextControl itemDescription;
    [SerializeField] private TextControl itemPrice;
    private readonly int maxCells = 12;
    private Player player;

    public bool HasSpace
    {
        get
        {
            return Items.Count < Cells.Count;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        Cells.AddRange(FindObjectsOfType<InventoryCell>());
        ClearItemInfo();
    }

    public void AddCell()
    {
        if (Cells.Count == maxCells) return;

        InventoryCell newCell = Instantiate(cellPrefab, cellsParent).GetComponent<InventoryCell>();
        Cells.Add(newCell);
        StartCoroutine(SetItemsPositions());
    }

    public void SetSelectedState(InventoryItem selectedItem, bool isSelected)
    {
        foreach (InventoryItem item in Items)
        {
            item.IsSelected = false;
        }
        selectedItem.IsSelected = isSelected;
        this.selectedItem = (selectedItem.IsSelected ? selectedItem : null);
        if (this.selectedItem != null) SetItemInfo();
        else ClearItemInfo();
    }

    public bool AddItem(ItemsManager.ItemsTypes type)
    {
        if (Items.Count + 1 > Cells.Count) return false;

        InventoryItem newItem = Instantiate(itemPrefab, itemsParent).GetComponent<InventoryItem>();
        newItem.transform.SetAsLastSibling();
        newItem.Type = type;
        Items.Add(newItem);
        Debug.Log("Item added to inventory: " + type.ToString());
        return true;
    }

    public void DropItem()
    {
        if (selectedItem == null) return;

        Item newItem = Instantiate(dropItemPrefab, FindObjectOfType<ItemsManager>().transform).GetComponent<Item>();
        Vector3 randomPosition = Quaternion.Euler(0, 0, Random.Range(-180, 180)) * transform.right * (Random.Range(0, 10)/10f);
        Vector3 playerPosition = FindObjectOfType<Player>().transform.position + randomPosition;
        newItem.transform.position = new Vector3(playerPosition.x, playerPosition.y, -0.2f);
        newItem.Type = selectedItem.Type;
        DeleteSelectedItem();
        Debug.Log("Drop item: " + newItem.Type.ToString());
    }

    public void DeleteSelectedItem()
    {
        ClearItemInfo();
        selectedItem.IsSelected = false;
        Items.Remove(selectedItem);
        Destroy(selectedItem.gameObject);
        selectedItem = null;
    }

    private IEnumerator SetItemsPositions()
    {
        yield return new WaitForEndOfFrame();

        foreach (InventoryItem item in Items)
        {
            item.transform.position = item.Cell.transform.position;
        }
    }

    public void Sell()
    {
        if (selectedItem == null) return;
        int index = (int)selectedItem.Type;
        player.Money += (int)(ItemsManager.Prices[index] * player.LootCost);
        DeleteSelectedItem();
        Debug.Log($"Sell item. Items count: {Items.Count}");
    }

    public void SetItemInfo()
    {
        int index = (int)selectedItem.Type;
        itemName.Text = ItemsManager.ItemsNames[index];
        itemDescription.Text = ItemsManager.ItemsDescriptions[index];
        itemPrice.Text = ((int)(ItemsManager.Prices[index] * player.LootCost)).ToString() + "$";
    }

    public void ClearItemInfo()
    {
        itemName.Clear();
        itemDescription.Clear();
        itemPrice.Clear();
    }

    public void Use()
    {
        if (selectedItem == null) return;
        int index = (int)selectedItem.Type;
        switch (selectedItem.Type)
        {
            case ItemsManager.ItemsTypes.Nothing:
                break;
            case ItemsManager.ItemsTypes.Medkit:
                player.MaxHP += 30;
                break;
            case ItemsManager.ItemsTypes.BigMedkit:
                player.Health += 100;
                break;
            case ItemsManager.ItemsTypes.Ammo:
                player.OfAmmo += 1;
                break;
            case ItemsManager.ItemsTypes.BigAmmo:
                player.OfAmmo += 3;
                break;
            case ItemsManager.ItemsTypes.Money:
                player.Money += (int)(ItemsManager.Prices[index] * player.LootCost);
                break;
            case ItemsManager.ItemsTypes.BigMoney:
                player.Money += (int)(ItemsManager.Prices[index] * player.LootCost);
                break;
        }
        DeleteSelectedItem();
    }
}
