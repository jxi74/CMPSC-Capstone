using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DialogueList : List<string> {}

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] OverWorldBox text;
    [SerializeField] List<String> dialogue;
    [SerializeField] private Boolean iterable;
    [SerializeField] private int flag;
    [SerializeField] private List<DialogueList> iterableDialogue;
    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor)
    {
        Debug.Log("NPC Interaction"); // Logs message once you press "e" to interact
        //List of list of dialogue strings
        if (!iterable)
        {
            foreach (String words in dialogue)
            {
                text.EnqueueSentence(words);
            }
            text.DisplayNextSentences();
        }
        else
        {
            if (flag != iterableDialogue.Count - 1)
            {
                //Iterate dialogue
                foreach (String words in iterableDialogue[flag])
                {
                    text.EnqueueSentence(words);
                }
                text.DisplayNextSentences();
                flag += 1;
            }
            else
            //Queues last dialogue over and over again
            {
                foreach (String words in iterableDialogue[flag - 1])
                {
                    text.EnqueueSentence(words);
                }
                text.DisplayNextSentences();
            }
        }

        
        
        return true;
    }
}
