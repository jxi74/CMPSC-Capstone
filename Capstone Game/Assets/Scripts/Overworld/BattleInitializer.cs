using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleInitializer : MonoBehaviour
{
    [SerializeField] private CharacterController charcontroller;
    public GameController gameController;
    public BattleSystem battlesystem;
    public GameObject unit1Prefab;
    public GameObject unit2Prefab;
    public GameObject unit4Prefab;
    public float prefabDistance = 7.5f; // Adjust the distance as desired


    public void InitializeBattle(Vector3 position, Vector3 position2, GameObject enemy)
    {
        charcontroller.enabled = false;
        Vector3 playerpos = position;
        Instantiate(unit1Prefab, playerpos, Quaternion.identity);
        unit1Prefab.tag = "Unit1";
        // Spawn the left prefab to the right of the given position
        Vector3 playerpos2 = position + transform.right * prefabDistance;
        Instantiate(unit2Prefab, playerpos2, Quaternion.identity);
        unit2Prefab.tag = "Unit2";


        enemy.tag = "Unit3";
        // Spawn the right prefab to the left of the given position
        Vector3 rightSpawnPosition = position2 - Vector3.right * prefabDistance;
        Instantiate(unit4Prefab, rightSpawnPosition, Quaternion.identity);
        unit4Prefab.tag = "Unit4";
        
        // Teleport the player to the average position of the four units
        Vector3 averagePosition = (playerpos + playerpos2 + rightSpawnPosition + enemy.transform.position) / 4f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = averagePosition;
        gameController.inBattle(true);
        
        battlesystem.unit1 = GameObject.FindWithTag("Unit1").GetComponent<BattleUnit>();
        battlesystem.unit2 = GameObject.FindWithTag("Unit2").GetComponent<BattleUnit>();
        battlesystem.unit3 = GameObject.FindWithTag("Unit3").GetComponent<BattleUnit>();
        battlesystem.unit4 = GameObject.FindWithTag("Unit4").GetComponent<BattleUnit>();
        battlesystem.party = player.GetComponent<Party>();
        battlesystem.enemygenerator = enemy.GetComponent<EnemyEncounter>();
        
        charcontroller.enabled = true;
        battlesystem.StartBattle();
    }
}
