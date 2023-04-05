using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum GameState { FreeRoam, Battle}

public class GameController : MonoBehaviour
{
    private GameState state;
    [SerializeField] private ThirdPersonMovement movement;
    [SerializeField] private CinemachineBrain thirdpersoncam;
    
    //[SerializeField] private CameraMotionControls battlecam;
    [SerializeField] private Camera cam;
    [SerializeField] private Inventory inventory;
    public BattleSystem battlesystem;
    private GameObject player;
    [SerializeField] private Party party;
    private Transform playerchar;
    private Renderer a;
    private Renderer b;
    
    
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioSource unitSwapAudioSource;
    [SerializeField] private AudioSource failAudioSource;
    [SerializeField] private AudioSource successAudioSource;
    [SerializeField] private AudioSource misc;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource unitNoises;
    [SerializeField] private AudioSource battleEffects;
    [SerializeField] private AudioSource statusEffects;
    [SerializeField] private AudioSource hitEffects;
    [SerializeField] private AudioClip overworldMusic;
    [SerializeField] private AudioClip battleMusic;
    [SerializeField] private AudioClip victoryAudio;
    [SerializeField] private AudioClip lossAudio;
    

    private void Awake()
    {
        ConditionsDB.Init();
    }
    
    private void Start()
    {
        //battlecam.enabled = false;
        player = GameObject.Find("Player");
        playerchar = player.transform.Find("Player character");
        a = playerchar.GetChild(0).GetComponent<Renderer>();
        b = playerchar.GetChild(0).GetChild(0).GetComponent<Renderer>();
    }

    public void Skill(AudioClip clip)
    {
        battleEffects.clip = clip;
        battleEffects.Play();
    }
    
    public void ButtonPress()
    {
        buttonAudioSource.Play();
    }

    public void UnitSwap()
    {
        unitSwapAudioSource.Play();
    }
    
    public void Fail()
    {
        failAudioSource.Play();
    }
    
    public void Success()
    {
        successAudioSource.Play();
    }

    public void Loss()
    {
        misc.clip = lossAudio;
        misc.Play();
    }

    public void Victory()
    {
        misc.clip = victoryAudio;
        misc.Play();
    }
    
    public void inBattle(bool inBattle)
    {
        if (inBattle)
        {
            bgm.clip = battleMusic;
            bgm.Play();
            state = GameState.Battle;
            //disable movement and hide player
            a.enabled = false;
            b.enabled = false;
            //battlecam.enabled = true;
            movement.enabled = false;
            //thirdpersoncam.enabled = false;
            battlesystem.enabled = true;
            inventory.canvas.enabled = false;
            inventory.enabled = false;
            cam.transform.LookAt(movement.transform);
            party.units.ForEach(p => p.OnBattleOver());
            
        }
        else
        {
            bgm.clip = overworldMusic;
            bgm.Play();
            state = GameState.FreeRoam;
            //enable movement and show player
            a.enabled = true;
            b.enabled = true;
            //battlecam.enabled = false;
            movement.enabled = true;
            //thirdpersoncam.enabled = true;
            battlesystem.enabled = false;
            inventory.enabled = true;
            cam.fieldOfView = 40;
            Destroy(GameObject.FindWithTag("Unit1"));
            Destroy(GameObject.FindWithTag("Unit2"));
            Destroy(GameObject.FindWithTag("Unit3"));
            Destroy(GameObject.FindWithTag("Unit4"));
            

        }
    }
    
}
