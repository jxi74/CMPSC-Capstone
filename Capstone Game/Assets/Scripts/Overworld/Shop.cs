using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private ShopUI shopUi;
    

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening Shop");
        shopUi.OpenUI();
        return true;
    }
}
