using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    private int index;

    public void SlotSelection()
    {
        // Get the child TextMeshPro object
        TextMeshProUGUI childTextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "Slot 1":
               // DataPersistenceManager.instance.currentSaveSlot = 0;
                break;
            case "Slot 2":
               // DataPersistenceManager.instance.currentSaveSlot = 1;
                break;
            case "Slot 3":
              //  DataPersistenceManager.instance.currentSaveSlot = 2;
                break;
        }
       // Debug.Log(DataPersistenceManager.instance.currentSaveSlot);
        
        // Check if the child TextMeshPro object's text is equal to something
        if (childTextMeshPro != null && childTextMeshPro.text == "NEW GAME")
        {
            //Sets text to current slot number
            MainMenuNewGame();
            childTextMeshPro.text = "LOAD GAME";
            Debug.Log("New Game");
        }
        else
        {
            MainMenuLoadGame();
            Debug.Log("Load Game");
        }
    }
    
    public void MainMenuNewGame()
    {
        levelLoader.LoadNextLevel();
        DataPersistenceManager.instance.NewGame();
    }

    public void MainMenuLoadGame()
    {
        levelLoader.LoadNextLevel();
        DataPersistenceManager.instance.LoadGame();
       // DataPersistenceManager.instance.SelectSaveSlot();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
