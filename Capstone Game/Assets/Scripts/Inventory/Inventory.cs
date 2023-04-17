using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//back end of the inventory which stores the data of what the character
//is currently holding
public class Inventory : MonoBehaviour, IDataPersistence
{
    [SerializeField] public int balance;
    public List<ItemBase> characterItems = new List<ItemBase>();
    [SerializeField] PartyHuds partyHud;
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    [SerializeField] private UIItem selectedItem;
    public Canvas canvas;
    [SerializeField] Tooltip tp;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            canvas.enabled = (!canvas.isActiveAndEnabled);
            canvas.GetComponent<CanvasGroup>().interactable = (canvas.isActiveAndEnabled);
            if (canvas.enabled)
            {
                
                inventoryUI.UpdateBalance(balance);
                partyHud.SetPartyNamesParty();
            }
        }
    }

    public void OpenUI()
    {
        canvas.enabled = true;
        canvas.GetComponent<CanvasGroup>().interactable = (canvas.isActiveAndEnabled);
        if (canvas.enabled)
        {
                
            inventoryUI.UpdateBalance(balance);
            partyHud.SetPartyNamesParty();
        }
    }

    public void CloseUI()
    {
        canvas.enabled = false;
        canvas.GetComponent<CanvasGroup>().interactable = (canvas.isActiveAndEnabled);
    }
    
    public void GiveItem(string itemName)
    {
        ItemBase itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
    }
    
    public void RemoveItem(string itemName)
    {
        ItemBase itemToRemove = characterItems.FirstOrDefault(item => item.Name == itemName);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);

            Debug.Log("Item removed: " + itemName);
        }
    }
    
    public void LoadData(GameData data)
    {
        this.balance = data.balance;
        this.characterItems = data.inventory;
        inventoryUI.PrepareInventory();
        canvas = inventoryUI.GetComponentInParent<Canvas>();
        tp.enabled = false;
        canvas.enabled = false;
        foreach (ItemBase item in data.inventory)
        {
            inventoryUI.AddNewItem(item);
        }
    }

    public void SaveData(GameData data)
    {
        data.balance = this.balance;
        data.inventory = this.characterItems;
    }
    
}