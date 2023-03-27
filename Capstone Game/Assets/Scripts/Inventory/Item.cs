using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Items", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemname;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public void Initialize(int id, string title, string description, Sprite icon, Dictionary<string, int> stats)
    {
        this.id = id;
        this.name = title;
        this.description = description;
        this.icon = icon;
        this.stats = stats;
    }
    
}