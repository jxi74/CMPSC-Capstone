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
    
    public bool Interact(Interactor interactor)
    {
        Debug.Log("NPC Interaction"); // Logs message once you press "e" to interact

        foreach (String words in dialogue)
        {
            text.EnqueueSentence(words);
        }
        text.DisplayNextSentences();

        return true;
    }
}
