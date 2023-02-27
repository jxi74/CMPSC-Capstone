using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer(PlayerInfo player)
    {
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        
    }
}
