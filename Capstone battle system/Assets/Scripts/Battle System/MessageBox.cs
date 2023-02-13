using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBox : MonoBehaviour
{
   [SerializeField] private int lettersPerSec;
   [SerializeField] private TextMeshProUGUI text;
   [SerializeField] private GameObject contButton;
   
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
         return;
      }

      StartCoroutine(DisplayText(Sentences.Dequeue()));
      
   }
   
   public IEnumerator DisplayText(String value)
   {
      contButton.SetActive(false);
      text.text = "";
      foreach (var letter in value.ToCharArray())
      {
         text.text += letter;
         yield return new WaitForSeconds(1f / lettersPerSec);
      }

      yield return new WaitForSeconds(3f);
      //contButton.SetActive(true);
      //Delay to make text readable
   }
   
}
