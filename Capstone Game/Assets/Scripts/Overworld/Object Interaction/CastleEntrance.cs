using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Door script for player to interact with door
public class CastleEntrance : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private AudioSource castledoor;
    [SerializeField] private LevelLoader levelLoader;

    public string InteractionPrompt => _prompt;
    
    public bool Interact(Interactor interactor) // Could have a check for the player's inventory to see if player has a key to open
    {
        Debug.Log("Opening door!"); // Logs message once you press "e" to open door
        castledoor.Play();
        levelLoader.LoadNextArea("EnterCastle");
        return true;
    }
}
