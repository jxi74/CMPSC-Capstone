using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//Door script for player to interact with door
public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject _playerObject;
    public string InteractionPrompt => _prompt;
    
    void Start()
    {
        _playerObject = GameObject.Find("Player");
    }
    
    public bool Interact(Interactor interactor) // Could have a check for the player's inventory to see if player has a key to open
    {
        Debug.Log("Interacting w/ enemy"); // Logs message once you press "e" to open door
        MovePlayerAway();
        return true;
    }
    
    public float distance = 5.0f; // distance to move the object

    void MovePlayerAway()
    {
        // calculate the direction from the current object to the player object
        Vector3 direction = _playerObject.transform.position - transform.position;

        // normalize the direction and multiply it by the desired distance
        Vector3 newPosition = _playerObject.transform.position + direction.normalized * distance;

        // move the player object to the new position
        _playerObject.transform.position = newPosition;
    }
}
