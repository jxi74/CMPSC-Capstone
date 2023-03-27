using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] public string description;
    [SerializeField] public Sprite icon;

    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;

    public virtual bool Use(Unit unit)
    {
        return false;
    }
}
