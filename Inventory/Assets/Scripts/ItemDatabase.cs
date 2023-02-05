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
            new Item(0, "Diamond Sword", "A sword made with diamond.",
            new Dictionary<string, int>
            {
                {"Power", 15 },
                {"Defense", 10 }
            }),
            new Item(1, "Diamond Pick", "A pickaxe made with diamond.",
            new Dictionary<string, int>
            {
                {"Power", 12},
            }),
            new Item(2, "Silver Pick", "A pickaxe made with silver.",
            new Dictionary<string, int>
            {
                {"Power", 8 }
            }),
            new Item(3, "Diamond Ore", "A beautiful diamond.",
            new Dictionary<string, int>
            {
                {"Value", 489 }
            }),
            new Item(4, "Gold Ore", "Some beautiful gold.",
            new Dictionary<string, int>
            {
                {"Value", 400 }
            }),
            new Item(5, "Emerald Ore", "A beautiful emerald",
            new Dictionary<string, int>
            {
                {"Value", 450 }
            })
          };
    }
}
