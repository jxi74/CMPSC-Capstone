using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] OverWorldBox text;
    [SerializeField] private int id;
    private bool opened = false;

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor) //Could have a check for the player's inventory to see if player has a key to open
    {
        if (!opened)
        {
            Debug.Log("Opening chest!"); // Logs message once you press "e" to open chest
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.GiveItem(id);
            text.EnqueueSentence("Opened chest!");
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
}
