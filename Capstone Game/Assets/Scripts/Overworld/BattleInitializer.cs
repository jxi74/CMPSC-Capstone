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
        // Unit 4
        Vector3 playerpos = position + Vector3.up * 2f;
        Instantiate(unit4Prefab, playerpos, Quaternion.identity);
        unit4Prefab.tag = "Unit4";
        
        Vector3 playerpos2 = position2 + Vector3.left * prefabDistance / 2f - Vector3.forward * prefabDistance / 2f + Vector3.up * 2f;
        Instantiate(unit1Prefab, playerpos2, Quaternion.identity);
        unit1Prefab.tag = "Unit1";
        


        enemy.tag = "Unit3";
        // Unit 2
        Vector3 rightSpawnPosition = position + Vector3.right * prefabDistance / 2f - Vector3.forward * prefabDistance / 2f + Vector3.up * 2f;
        Instantiate(unit2Prefab, rightSpawnPosition, Quaternion.identity);
        unit2Prefab.tag = "Unit2";
        
        // Teleport the player to the average position of the four units
        Vector3 averagePosition = (playerpos + playerpos2 + rightSpawnPosition + enemy.transform.position) / 4f  + Vector3.up * 1.5f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = averagePosition;
        gameController.inBattle(true);
        
        unit1Prefab.transform.LookAt(player.transform);
        unit2Prefab.transform.LookAt(player.transform);
        enemy.transform.LookAt(player.transform);
        unit4Prefab.transform.LookAt(player.transform);
        
        
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
