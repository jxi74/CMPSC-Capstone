using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//back end of the inventory which stores the data of what the character
//is currently holding
public class Inventory : MonoBehaviour
{
    [SerializeField] public int balance;
    public List<ItemBase> characterItems = new List<ItemBase>();
    [SerializeField] PartyHuds partyHud;
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    [SerializeField] private UIItem selectedItem;
    public Canvas canvas;
    [SerializeField] Tooltip tp;

    private void Start()
    {
        inventoryUI.PrepareInventory();
        canvas = inventoryUI.GetComponentInParent<Canvas>();
        tp.enabled = false;
        canvas.enabled = false;
        GiveItem("Bandage");
        GiveItem("Energy Drink");
        GiveItem("Energy Drink");
    }

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
    
}