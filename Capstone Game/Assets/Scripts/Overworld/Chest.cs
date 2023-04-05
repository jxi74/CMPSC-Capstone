using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] OverWorldBox text;
    [SerializeField] private int id;
    [SerializeField] private ItemBase item;
    [SerializeField] private int itemno;
    [SerializeField] private int money;
    [SerializeField] private ChestType type;
    private bool opened = false;

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor) //Could have a check for the player's inventory to see if player has a key to open
    {
        transform.GetComponent<AudioSource>().Play();
        if (!opened)
        {
            Debug.Log("Opening chest!"); // Logs message once you press "e" to open chest
            Inventory inventory = FindObjectOfType<Inventory>();
            
            text.EnqueueSentence("Opened chest!");
            if (type == ChestType.Money)
            {
                inventory.balance += money;
                text.EnqueueSentence($"You found {money} gold!");
            }
            else
            {
                
                for (int i = 0; i < itemno; i++)
                {
                    inventory.GiveItem(item.name);
                }
                text.EnqueueSentence($"You found {itemno}x {((Object)item).name}!");
            }
            text.DisplayNextSentences();
            opened = true;

            //Replace object with opened chest prefab
        }
        else
        {
            text.EnqueueSentence("Chest already opened!");
            text.DisplayNextSentences();
        }
        
        
        
        return true;
    }
    
    public enum ChestType
    {
        Money,
        Consumable,
        Battle
    }
}
