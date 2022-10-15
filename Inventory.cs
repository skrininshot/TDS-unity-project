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
    private readonly int maxCells = 12;

    public bool HasSpace
    {
        get
        {
            return Items.Count < Cells.Count;
        }
    }
    private void Start()
    {
        Cells.AddRange(FindObjectsOfType<InventoryCell>());
        Debug.Log("Start count of cells in inventory: " + Cells.Count);
    }

    public void AddCell()
    {
        if (Cells.Count == maxCells) return;

        InventoryCell newCell = Instantiate(cellPrefab, cellsParent).GetComponent<InventoryCell>();
        Cells.Add(newCell);
        StartCoroutine(SetItemsPositions());
    }

    #region debug methods
    public void AddItem()
    {
        if (Items.Count + 1 > Cells.Count) return;

        InventoryItem newItem = Instantiate(itemPrefab, itemsParent).GetComponent<InventoryItem>();
        newItem.transform.SetAsLastSibling();
        newItem.Type = ItemsManager.ItemsTypes.Nothing;
        Items.Add(newItem);
        Debug.Log("Item added to inventory: " + newItem.Type.ToString());
    }

    public void DeleteCell()
    {
        if (Cells.Count == 0) return;

        InventoryCell deleteCell = Cells[Cells.Count - 1];
        if (deleteCell.Item != null) DeleteInventoryItem(deleteCell.Item);
        Cells.Remove(deleteCell);
        Destroy(deleteCell.gameObject);
        StartCoroutine(SetItemsPositions());
    }
    #endregion

    public void SetSelectedState(InventoryItem selectedItem, bool isSelected)
    {
        foreach (InventoryItem item in Items)
        {
            item.IsSelected = false;
        }
        selectedItem.IsSelected = isSelected;
        this.selectedItem = (selectedItem.IsSelected ? selectedItem : null);
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
        selectedItem.Cell.SetSelectedState(false);
        DeleteInventoryItem(selectedItem);
        selectedItem = null;
        Debug.Log("Drop item: " + newItem.Type.ToString());
    }

    public void DeleteInventoryItem(InventoryItem item)
    {
        Items.Remove(item);
        Destroy(item.gameObject);  
    }

    private IEnumerator SetItemsPositions()
    {
        yield return new WaitForEndOfFrame();

        foreach (InventoryItem item in Items)
        {
            item.transform.position = item.Cell.transform.position;
        }
    }
}
