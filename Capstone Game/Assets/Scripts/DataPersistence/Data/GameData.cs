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
    public Quaternion playerRotation;
    public SerializableDictionary<string, bool> chestsOpened;
    public SerializableDictionary<string, int> npcFlag;
    public List<Unit> party;
    public int balance;
    public List<ItemBase> inventory;
    public float volume;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        playerPosition = new Vector3(500.8f, 23.45f, 324.9f);
        playerRotation = Quaternion.identity;
        chestsOpened = new SerializableDictionary<string, bool>();
        npcFlag = new SerializableDictionary<string, int>();
        party = new List<Unit>();
        balance = 7500;
        inventory = new List<ItemBase>();
        volume = 4;
        Debug.Log("Values reset");
        
        
    }
}