using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

    public void MainMenuNewGame()
    {
        levelLoader.LoadNextLevel();
        DataPersistenceManager.instance.NewGame();
    }

    public void MainMenuLoadGame()
    {
        levelLoader.LoadNextLevel();
        DataPersistenceManager.instance.LoadGame();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
