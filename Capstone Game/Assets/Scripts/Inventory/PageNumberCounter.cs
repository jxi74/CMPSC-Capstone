using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageNumberCounter : MonoBehaviour
{
    public TextMeshProUGUI pageCounter;
    private int counter = 1;

    void Start()
    {
        pageCounter.text = counter + "/4";
    }
    public void NextClick()
    {
        counter++;
        pageCounter.text = counter + "/4";
    }
    public void PrevClick()
    {
        counter--;
        pageCounter.text = counter + "/4";
    }
}
