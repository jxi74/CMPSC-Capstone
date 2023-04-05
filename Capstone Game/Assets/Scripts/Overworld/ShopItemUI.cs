using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopItemUI : MonoBehaviour
{
    public ShopItem shopitem;
    [SerializeField] private TextMeshProUGUI itemname;
    [SerializeField] private TextMeshProUGUI price;

    public void Initialize()
    {
        itemname.text = shopitem.displayName;
        price.text = shopitem.price.ToString();
    }
    
    public void ButtonPress()
    {
        GameObject.Find("GameController").GetComponent<GameController>().ButtonPress();
    }
    
    public void Purchase()
    {
        ShopUI shop = GameObject.Find("ShopUI").GetComponent<ShopUI>();
        if (shopitem.type == ShopItem.ItemType.Item)
        {
            shop.BuyItem(shopitem.ItemName, shopitem.price);
        }
        else if (shopitem.type == ShopItem.ItemType.Unit)
        {
            shop.BuyUnit(shopitem.unit, shopitem.price);
        }
    }



}
