using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI display of what the player currently holds in their inventory
public class UIInventory : MonoBehaviour
{
    //list of all items currently in player's inventory
    public List<UIItem> uIItems = new List<UIItem>();

    //creates a slot game object using the prefab established
    public GameObject slotPrefab;

    //creates the panel in which the slots will be added
    public Transform slotPanel;

    //the number of slots allowed in the inventory
    public int numberOfSlots = 16;
    private void Awake()
    {
        //instantites the layout of the inventory by adding the
        //number of desired inventory slots to the inventory panel
        for(int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    //updates the desired slot of the uIItems list with the desired item
    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
    }

    //adds a new item to the next open slot in the inventory
    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    //removes the first instance of an item from the current invnetory
    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
}
