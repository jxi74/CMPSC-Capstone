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
    //a list that contains all of the items that the character is holding
    public List<Item> characterItems = new List<Item>();
    [SerializeField] PartyHuds partyHud;
    //a database that contains all possible items
    public ItemDatabase itemDatabase;

    //the display of the inventory that the player can see
    public UIInventory inventoryUI;
    public Canvas canvas;
    [SerializeField] Tooltip tp;
    private void Start()
    {
        canvas = inventoryUI.GetComponentInParent<Canvas>();
        tp.enabled = false;
        canvas.enabled = false;
        
        //Give dummy items
        GiveItem(4);
        GiveItem(1);
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            canvas.enabled = (!canvas.isActiveAndEnabled);
            if (canvas.enabled)
            {
                inventoryUI.UpdateBalance(balance);
                partyHud.SetPartyNamesParty();
            }
            //inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
            //partyUI.gameObject.SetActive(!partyUI.gameObject.activeSelf);
        }
    }

    //adds the item to the player's Inventory and UIInventory using
    //the id value of the item
    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
    }

    //adds the item to the player's Inventory and UIInventoryy using
    //the name value of the item
    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
    }

    //returns whether the character is currently holding a specific
    //item in their inventory
    public Item CheckForItem(int id)
    {
        return characterItems.Find(Item => Item.id == id);
    }

    //removes an item from the characters inventory using the
    //id of that item
    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            
            Debug.Log("Item removed: " + itemToRemove.title);
        }
    }
}
