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
    private Coroutine currentCoroutine;
    
    public Queue<String> Sentences = new Queue<string>();
    
    public void SetText(String value)
    {
       text.text = value;
    }
    
    

    public void EnqueueSentence(string value)
    {
       Sentences.Enqueue(value);

       // If there isn't a sentence currently being displayed, start displaying the next sentence.
       if (currentCoroutine == null)
       {
          DisplayNextSentences();
       }
    }

    public void DisplayNextSentences()
    {
       if (Sentences.Count == 0)
       {
          overworldbox.enabled = false;
          return;
       }

       // Stop the current typing coroutine if one is running.
       if (currentCoroutine != null)
       {
          StopCoroutine(currentCoroutine);
       }

       currentCoroutine = StartCoroutine(DisplayText(Sentences.Dequeue()));
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
