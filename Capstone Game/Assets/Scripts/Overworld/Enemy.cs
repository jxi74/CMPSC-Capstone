using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    //[SerializeField] public ThirdPersonMovement playerMovement; // reference to ThirdPersonMovement script
    [SerializeField] public int distance;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with enemy");
        ThirdPersonMovement playerMovement = interactor.GetComponent<ThirdPersonMovement>();
        playerMovement.enabled = false;
    
        // Get the direction the player is facing
        Vector3 direction = interactor.transform.forward;

        // Move the player backwards in the XZ plane
        interactor.GetComponent<ThirdPersonMovement>().MovePlayer(-direction, distance);

        BattleInitializer initializer = FindObjectOfType<BattleInitializer>();
        
        //Initialize battle
        initializer.InitializeBattle(interactor.transform.position, transform.position, gameObject);
        
        playerMovement.enabled = true;

        return true;
    }
}