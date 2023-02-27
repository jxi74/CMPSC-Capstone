using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] OverWorldBox text;
    [SerializeField] List<String> dialogue;

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor) //Could have a check for the player's inventory to see if player has a key to open
    {
        Debug.Log("NPC Interaction"); // Logs message once you press "e" to open chest

        foreach (String words in dialogue)
        {
            text.EnqueueSentence(words);
            //yield return new WaitForSeconds(2);
        }
        text.DisplayNextSentences();

        return true;
    }
}
