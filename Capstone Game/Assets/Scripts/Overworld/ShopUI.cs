using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private OverWorldBox box;
    [SerializeField] private Canvas shopUi;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Party party;
    [SerializeField] private TextMeshProUGUI balance;
    [SerializeField] public ShopItemUI ShopItem;
    [SerializeField] public GameObject ShopContent; 
    [SerializeField] private Unit unit;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip successPurchase;
    [SerializeField] private AudioClip failPurchase;
    
    
    public void OpenUI(List<ShopItem> items)
    {
        balance.text = string.Format("{0:#,###0}", inventory.balance);
        
        //initialize items
        foreach (ShopItem item in items)
        {
            ShopItemUI shopItemUIGameObject = Instantiate(ShopItem, ShopContent.transform);
            shopItemUIGameObject.shopitem = item;
            shopItemUIGameObject.Initialize();
        }
        
        shopUi.enabled = true;
    }

    public void CloseUI()
    {
        //reset shop
        foreach (Transform child in ShopContent.transform)
        {
            Destroy(child.gameObject);
        }
        shopUi.enabled = false;
    }

    
    
    public void BuyItem(string name, int price)
    {
        if (inventory.balance >= price)
        {
            audioSource.clip = successPurchase;
            audioSource.Play();
            inventory.balance -= price;
            inventory.GiveItem(name);
            balance.text = string.Format("{0:#,###0}", inventory.balance);
        }
        else
        {
            audioSource.clip = failPurchase;
            audioSource.Play();
            //StartCoroutine(box.DisplayText("Not enough money!"));

        }
    }
    public void BuyUnit(Unit unit, int price)
    {
        if (inventory.balance >= price)
        {
            inventory.balance -= price;
            
            //Can randomize later
            unit.Init();
            party.units.Add(unit);
            
            balance.text = string.Format("{0:#,###0}", inventory.balance);
        }
        else
        {
            audioSource.clip = failPurchase;
            audioSource.Play();
            //StartCoroutine(box.DisplayText("Not enough money!"));
        }
    }
}
