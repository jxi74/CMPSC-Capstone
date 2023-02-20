using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor) //Could have a check for the player's inventory to see if player has a key to open
    {
        Debug.Log("NPC Interaction"); // Logs message once you press "e" to open chest
        return true;
    }
}
