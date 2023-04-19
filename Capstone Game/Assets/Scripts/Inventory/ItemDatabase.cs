using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<ItemBase> items = new List<ItemBase>();
    
    private void Awake()
    {
        BuildDatabase();
    }
    

    public ItemBase GetItem(string itemName)
    {
        return items.Find(item => item.name == itemName);
    }
    
    void BuildDatabase()
    {
        // Add the Bandage item to the items list.
        items.Add(Resources.Load<ItemBase>("Items/Bandage"));
        items.Add(Resources.Load<ItemBase>("Items/Energy Drink"));
        items.Add(Resources.Load<ItemBase>("Items/Elixir"));
        items.Add(Resources.Load<ItemBase>("Items/Small Elixir"));
        items.Add(Resources.Load<ItemBase>("Items/Yggdrasil drop"));
        items.Add(Resources.Load<ItemBase>("Items/Blood of Gods"));
        items.Add(Resources.Load<ItemBase>("Items/Revive"));
        items.Add(Resources.Load<ItemBase>("Items/All Purpose Medicine"));

        // Add any other items to the items list here.
    }
    
}