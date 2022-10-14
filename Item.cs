using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactive
{
    public ItemsManager.ItemsTypes Type
    {
        get
        {
            return type;
        }
        set
        {
            SetItem(value);
        }
    }
    [SerializeField] private ItemsManager.ItemsTypes type;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        SetItem(type);
    }

    private void SetItem(ItemsManager.ItemsTypes newType)
    {
        Sprite[] sprites = FindObjectOfType<ItemsManager>().ItemsSprites;
        type = newType;
        sprite.sprite = sprites[(int)type];
    }

    public override void Interact()
    {
        Debug.Log("Interact with " + name);
        if (FindObjectOfType<Inventory>().AddItem(type)) 
        {
            Destroy(gameObject);
        }
    }
}
