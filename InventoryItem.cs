using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    [HideInInspector] public InventoryCell Cell
    {
        get { return cell; }
        set { ChangeCell(value); }
    }
    private InventoryCell cell = null;
    private Vector2 addedDistance;
    private bool isClicked;
    public ItemsManager.ItemsTypes Type
    {
        get { return type; }
        set { SetType(value); }
    }
    private ItemsManager.ItemsTypes type;
    private Image sprite;
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            cell.SetSelectedState(value);
            isSelected = value;
        }
    }
    private bool isSelected;

    private void Awake()
    {
        SetCellOnStart(GetNearestCell());
        sprite = GetComponent<Image>();
    }

    private void SetType(ItemsManager.ItemsTypes newType)
    {
        Sprite[] sprites = FindObjectOfType<ItemsManager>().GetComponent<ItemsManager>().ItemsSprites;
        type = newType;
        sprite.sprite = sprites[(int)type];
        sprite.SetNativeSize();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isClicked = true;
       
        addedDistance = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isClicked = false;

        ChangeCell(GetNearestCell());
        FindObjectOfType<Inventory>().SetSelectedState(this, !isSelected);  
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        if (!isClicked) return;
        transform.position = pointerEventData.position + addedDistance;
        FindObjectOfType<Inventory>().SetSelectedState(this, false);
    }

    private void ChangeCell(InventoryCell newCell)
    {
        transform.position = newCell.transform.position;
        if (cell == newCell) return;
        cell.Item = null;
        cell = newCell;
        cell.Item = this;
    }

    public void SetCellOnStart(InventoryCell newCell)
    {
        transform.position = newCell.transform.position;
        cell = newCell;
        cell.Item = this;
    }

    private InventoryCell GetNearestCell()
    {
        if (FindObjectOfType<Inventory>().Cells.Count == 0) return null;

        float minDistance = Mathf.Infinity;
        InventoryCell nearest = null;
        foreach (InventoryCell potentialNearest in FindObjectOfType<Inventory>().Cells)
        {
            Vector2 potentialPosition = potentialNearest.transform.position;
            Vector2 position = transform.position;
            float currentDistance = Vector2.Distance(potentialPosition, position);
            if (currentDistance < minDistance && (potentialNearest.Item == null || potentialNearest.Item == this))
            {
                minDistance = currentDistance;
                nearest = potentialNearest;
            }
        }
        return nearest;
    }
}
