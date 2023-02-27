using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int currLevel;
    public int unitHealth1;
    public int unitHealth2;
    public float[] position;

    public PlayerData (PlayerInfo player)
    {
        currLevel = player.currLevel;
        unitHealth1 = player.unitHealth1;
        unitHealth2 = player.unitHealth2;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
