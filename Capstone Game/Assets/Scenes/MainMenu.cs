using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Replace GetActiveScene w/ battle test 
        SceneManager.LoadScene("Battle test");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
