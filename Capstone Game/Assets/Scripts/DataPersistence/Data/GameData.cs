using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    //public int deathCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> chestsOpened;
    public List<Unit> party;
    public int balance;
    public List<ItemBase> inventory;
    public float volume;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        playerPosition = new Vector3(500.8f, 23.45f, 324.9f);
        chestsOpened = new SerializableDictionary<string, bool>();
        party = new List<Unit>();
        balance = 15000;
        inventory = new List<ItemBase>();
        volume = 4;
        Debug.Log("Values reset");
    }
}