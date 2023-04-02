using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public float transitionTime2 = .5f;
    
    [SerializeField] private ThirdPersonMovement thirdPersonMovement;

    // Update is called once per frame

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadNextArea(String location)
    {
        StartCoroutine(LoadArea(location));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    
    IEnumerator LoadArea(String location)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Teleport player to new Area
        switch (location)
        {
            case "EnterCastle":
                //Teleport into Castle
                thirdPersonMovement.SetPlayerPosition(new Vector3(214.74f, 22.113f, 644.27f), Quaternion.Euler(0f, -30f, 0f));
                break;
            case "ExitCastle":
                //Teleport out of Castle
                thirdPersonMovement.SetPlayerPosition(new Vector3(233.29f, 22.57f, 625.76f), Quaternion.Euler(0f, 124.9654f, 0f));
                break;
        }

        yield return new WaitForSeconds(transitionTime2);
        
        transition.SetTrigger("End");
    }
}
