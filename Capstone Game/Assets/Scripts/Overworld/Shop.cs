using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private ShopUI shopUi;
    [SerializeField] public List<ShopItem> items;
    
    

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor)
    {
        if (!shopUi.GetComponent<Canvas>().enabled && !FindObjectOfType<PauseMenu>().GameIsPaused)
        {
            transform.GetComponent<AudioSource>().Play();
            Debug.Log("Opening Shop");
        
            shopUi.OpenUI(items);
        }
        
        return true;
    }
}
