using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverWorldBox : MonoBehaviour
{
    [SerializeField] private Canvas overworldbox;
    [SerializeField] private int lettersPerSec;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject contButton;
    [SerializeField] private GameObject exitButton;
    
    public Queue<String> Sentences = new Queue<string>();
    
    public void SetText(String value)
    {
       text.text = value;
    }
    
    public void EnqueueSentence(String value)
    {
       Sentences.Enqueue(value);
       //Debug.Log("Sentence Enqueued!");
    }

    public void DisplayNextSentences()
    {
      
       if (Sentences.Count == 0)
       {
          //Debug.Log("No more");
          overworldbox.enabled = false;
          return;
       }

       StartCoroutine(DisplayText(Sentences.Dequeue()));
      
    }
    
    public IEnumerator DisplayText(String value)
    {
       overworldbox.enabled = true;
       contButton.SetActive(false);
       exitButton.SetActive(false);
       text.text = "";
       foreach (var letter in value.ToCharArray())
       {
          text.text += letter;
          yield return new WaitForSeconds(1f / lettersPerSec);
       }
 
       yield return new WaitForSeconds(1f);
       
       if (Sentences.Count == 0)
       {
          exitButton.SetActive(true);
       }
       else
       {
          contButton.SetActive(true);
       }
       //Delay to make text readable
       //Use this code to enable elements
    }
}
