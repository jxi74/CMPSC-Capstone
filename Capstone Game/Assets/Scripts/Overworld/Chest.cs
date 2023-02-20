using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor) //Could have a check for the player's inventory to see if player has a key to open
    {
        Debug.Log("Opening chest!"); // Logs message once you press "e" to open chest
        return true;
    }
}
