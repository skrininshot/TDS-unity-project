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
        SetRandomRotation();
        SetRandomType();
        SetItem(type);
    }

    private void SetRandomRotation()
    {
        transform.rotation = Quaternion.Euler(0,0, Random.Range(-181, 181));
    }

    private void SetRandomType()
    {
        ItemsManager.ItemsTypes[] randomItem = {
            ItemsManager.ItemsTypes.Medkit,
            ItemsManager.ItemsTypes.Medkit,
            ItemsManager.ItemsTypes.Medkit,

            ItemsManager.ItemsTypes.BigMedkit,

            ItemsManager.ItemsTypes.Ammo,
            ItemsManager.ItemsTypes.Ammo,
            ItemsManager.ItemsTypes.Ammo,

            ItemsManager.ItemsTypes.BigAmmo,

            ItemsManager.ItemsTypes.Money,
            ItemsManager.ItemsTypes.BigMoney,
        };

        int randomType = Random.Range(1, randomItem.Length);
        type = randomItem[randomType];
    }

    private void SetItem(ItemsManager.ItemsTypes newType)
    {
        Sprite[] sprites = FindObjectOfType<ItemsManager>().ItemsSprites;
        type = newType;
        sprite.sprite = sprites[(int)type];
    }

    public override void Interact()
    {
        if (FindObjectOfType<Inventory>().AddItem(type)) 
        {
            Destroy(gameObject);
        }
    }
}