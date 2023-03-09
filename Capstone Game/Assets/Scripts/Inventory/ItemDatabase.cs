using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }
    void BuildDatabase()
    {
        items = new List<Item> {
           new Item(0, "Rusty Sword", "A rusty worn out sword.", "equipable", null,
           new Dictionary<string, int>
           {
               {"ATK", 3}
           }),
           new Item(1, "Cardboard Shield", "An item used to defend against enemies.", "equipable", null,
           new Dictionary<string, int>
           {
               {"DEF", 3}
           }),
           new Item(2, "Mysterious Stick", "A stick! But mysterious.....", "equipable", null,
           new Dictionary<string, int>
           {
               {"FLX", 5}
           }),
           new Item(3, "Energy Drink", "Regenerate STA by 15", "consumable", 
           new Dictionary<string, int>
           {
               {"Regenerate STA", 15 }
           },
           new Dictionary<string, int>
           {

           }),
           new Item(4, "Bandage", "Regenerates HP by 10", "consumable", 
           new Dictionary<string, int>
           {
               {"Regenerate HP", 10 }
           }
           
               , new Dictionary<string, int>
               {

               }),
           new Item(5, "Wumbo", "I wumbo, you wumbe, he, she, we wumbo", "equipable", null,
           new Dictionary<string, int>
           {
               {"HP", 25}
           })
          };
    }
}