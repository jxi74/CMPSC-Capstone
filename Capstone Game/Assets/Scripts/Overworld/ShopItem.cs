using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Items", menuName = "Shop/Create Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] public string displayName;
    [SerializeField] public int price;
    [SerializeField] public string itemName;
    [SerializeField] public Unit unit;
    [SerializeField] public ItemType type;

    public string Name
    {
        get
        {
            return displayName;
        }
    }
    
    public int Price
    {
        get
        {
            return price;
        }
    }

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public Unit Unit
    {
        get
        {
            return unit;
        }
    }

    public enum ItemType
    {
        Item,
        Unit
    }
}
