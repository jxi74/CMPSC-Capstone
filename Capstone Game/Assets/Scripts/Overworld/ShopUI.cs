using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private OverWorldBox box;
    [SerializeField] private Canvas shopUi;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Party party;
    [SerializeField] private TextMeshProUGUI balance;

    [SerializeField] private Unit unit;
    public void OpenUI()
    {
        balance.text = string.Format("{0:#,###0}", inventory.balance);
        shopUi.enabled = true;
    }

    public void CloseUI()
    {
        shopUi.enabled = false;
    }

    //Hard coded to test functionality, remove later
    public void BuyButtons(int id)
    {
        if (id == 1)
        {
            BuyItem("String", 250);
        }
        else
        {
            BuyUnit(unit, 10000);
        }
    }

    public void BuyItem(string name, int price)
    {
        if (inventory.balance >= price)
        {
            inventory.balance -= price;
            inventory.GiveItem(name);
            balance.text = string.Format("{0:#,###0}", inventory.balance);
        }
        else
        {
            StartCoroutine(box.DisplayText("Not enough money!"));

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
            StartCoroutine(box.DisplayText("Not enough money!"));
        }
    }
}
