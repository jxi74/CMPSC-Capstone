using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public enum GameState { FreeRoam, Battle}

public class GameController : MonoBehaviour
{
    private GameState state;
    [SerializeField] private ThirdPersonMovement movement;
    [SerializeField] private CinemachineBrain thirdpersoncam;
    [SerializeField] private CameraMotionControls battlecam;
    [SerializeField] private Camera cam;
    public BattleSystem battlesystem;
    private GameObject player;
    [SerializeField] private Party party;
    private Transform playerchar;
    private Renderer a;
    private Renderer b;
    private void Start()
    {
        player = GameObject.Find("Player");
        playerchar = player.transform.Find("Player character");
        a = playerchar.GetChild(0).GetComponent<Renderer>();
        b = playerchar.GetChild(0).GetChild(0).GetComponent<Renderer>();
    }
    
    public void inBattle(bool inBattle)
    {
        if (inBattle)
        {
            //disable movement and hide player
            a.enabled = false;
            b.enabled = false;
            battlecam.enabled = true;
            movement.enabled = false;
            thirdpersoncam.enabled = false;
            battlesystem.enabled = true;
            cam.transform.LookAt(movement.transform);
            party.units.ForEach(p => p.OnBattleOver());
            
        }
        else
        {
            //enable movement and show player
            a.enabled = true;
            b.enabled = true;
            battlecam.enabled = false;
            movement.enabled = true;
            thirdpersoncam.enabled = true;
            battlesystem.enabled = false;
            cam.fieldOfView = 40;
            Destroy(GameObject.FindWithTag("Unit1"));
            Destroy(GameObject.FindWithTag("Unit2"));
            Destroy(GameObject.FindWithTag("Unit3"));
            Destroy(GameObject.FindWithTag("Unit4"));
            

        }
    }
    
}
