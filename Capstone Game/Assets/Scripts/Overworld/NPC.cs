using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueList
{
    [SerializeField]
    public List<string> myList;
}

public class NPC : MonoBehaviour, IInteractable, IDataPersistence
{
    //Unique ID for chest
    [SerializeField] private string npcID;

    [ContextMenu("Generate guid for id")]

    //Used to Create Unique ID for each Chest Object
    private void GenerateGuid()
    {
        npcID = System.Guid.NewGuid().ToString();
    }
    
    [SerializeField] private string _prompt;

    [SerializeField] OverWorldBox text;
    //[SerializeField] List<String> dialogue;
    [SerializeField] private Boolean iterable = true;
    [SerializeField] private int flag;
    [SerializeField] private List<DialogueList> iterableDialogue = new List<DialogueList>();
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("NPC Interaction"); // Logs message once you press "e" to interact
        //List of list of dialogue strings
        if (iterable)
        //{
        //    foreach (String words in dialogue)
        //    {
        //       text.EnqueueSentence(words);
        //    }
        //    text.DisplayNextSentences();
        //}
       // else 
       // {
            //If flag does not equal last dialogue
            if (flag != iterableDialogue.Count - 1)
            {
                //Iterate dialogue
                foreach (String words in iterableDialogue[flag].myList)
                {
                    text.EnqueueSentence(words);
                }
                text.DisplayNextSentences();
                flag += 1;
                Debug.Log(iterableDialogue.Count);
            }
            else
            //Queues last dialogue over and over again
            {
                foreach (String words in iterableDialogue[flag].myList)
                {
                    text.EnqueueSentence(words);
                }
                text.DisplayNextSentences();
                //iterable = false;
            }

        return true;
    }
    
    public void LoadData(GameData data)
    {
        data.npcFlag.TryGetValue(npcID, out flag);
    }

    public void SaveData(GameData data)
    {
        if (data.npcFlag.ContainsKey(npcID)) //If ID is already in dictionary
        {
            data.npcFlag.Remove(npcID);
        }
        data.npcFlag.Add(npcID, flag);
    }
}
