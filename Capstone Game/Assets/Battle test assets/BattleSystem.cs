using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState {start, player, enemy, won, lost }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject player;
    public GameObject enemy;

	unit_data playerUnit;
	unit_data enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.start;
		setup();
    }

    // Update is called once per frame
    void setup()
    {	
	
        GameObject playerstuff = Instantiate(player);
		playerUnit = playerstuff.GetComponent<unit_data>();

		GameObject enemystuff = Instantiate(enemy);
		enemyUnit = enemystuff.GetComponent<unit_data>();
    }
}
