using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum GameState { FreeRoam, Battle}

public class GameController : MonoBehaviour
{
    private GameState state;
    [SerializeField] private GameObject enemyPool1;
    [SerializeField] private ThirdPersonMovement movement;
    [SerializeField] private CinemachineBrain thirdpersoncam;
    [SerializeField] private GameObject interactionpoint;
    
    //[SerializeField] private CameraMotionControls battlecam;
    [SerializeField] private Camera cam;
    [SerializeField] private Inventory inventory;
    public BattleSystem battlesystem;
    public Canvas battleUI;
    private GameObject player;
    [SerializeField] private Party party;
    private Transform playerchar;
    private GameObject playermodel;
    private Renderer a;
    private Renderer b;
    private GameObject emptyInstance;
    [SerializeField] private GameObject switcheffect;
    
    
    
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
        emptyInstance = Instantiate(enemyPool1, enemyPool1.transform.position, Quaternion.identity);
        // Loop through all child objects and instantiate them
        //battlecam.enabled = false;
        player = GameObject.Find("Player");
        playerchar = player.transform.Find("Player character");
        PlayerModelReset();
        //a = playerchar.GetChild(0).GetComponent<Renderer>();
        //b = playerchar.GetChild(0).GetChild(0).GetComponent<Renderer>();
    }

    public void PlayerModelReset()
    {
        playerchar = player.transform.Find("Player character");
        Destroy(playermodel);
        playermodel = Instantiate(party.units[0].Base.Model, playerchar.transform.position + Vector3.down * 1.8f, playerchar.rotation);
        playermodel.transform.SetParent(playerchar.transform);
        Destroy(playermodel.GetComponent<Rigidbody>());

        switcheffect.transform.localScale = new Vector3(2, 2, 2);
        Instantiate(switcheffect, playermodel.transform.position + Vector3.up * 1.25f, playermodel.transform.rotation);
        
        ParticleSystem ps = switcheffect.GetComponent<ParticleSystem>();
        Destroy(switcheffect, ps.main.duration);
    }
    
    public void ResetUnits()
    {
        // Destroy the existing instance of enemyPool1
        Destroy(emptyInstance);

        // Create a new instance of the prefab
        GameObject newEnemyPool1 = Instantiate(enemyPool1, enemyPool1.transform.position, Quaternion.identity);

        // Set the reference to the new instance
        emptyInstance = newEnemyPool1;
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
            interactionpoint.SetActive(false);
            bgm.clip = battleMusic;
            bgm.Play();
            state = GameState.Battle;
            //disable movement and hide player
            //a.enabled = false;
            //b.enabled = false;
            playermodel.gameObject.SetActive(false);
            //battlecam.enabled = true;
            movement.enabled = false;
            //thirdpersoncam.enabled = false;
            battlesystem.enabled = true;
            battleUI.GetComponent<CanvasGroup>().interactable = true;
            inventory.canvas.enabled = false;
            inventory.enabled = false;
            cam.transform.LookAt(movement.transform);
            party.units.ForEach(p => p.OnBattleOver());
            
        }
        else
        {
            interactionpoint.SetActive(true);
            bgm.clip = overworldMusic;
            bgm.Play();
            state = GameState.FreeRoam;
            //enable movement and show player
            //a.enabled = true;
            //b.enabled = true;
            playermodel.gameObject.SetActive(true);
            //battlecam.enabled = false;
            movement.enabled = true;
            //thirdpersoncam.enabled = true;
            battlesystem.enabled = false;
            battleUI.GetComponent<CanvasGroup>().interactable = false;
            inventory.enabled = true;
            cam.fieldOfView = 40;
            Destroy(GameObject.FindWithTag("Unit1"));
            Destroy(GameObject.FindWithTag("Unit2"));
            Destroy(GameObject.FindWithTag("Unit3"));
            Destroy(GameObject.FindWithTag("Unit4"));
            

        }
    }
    
}
