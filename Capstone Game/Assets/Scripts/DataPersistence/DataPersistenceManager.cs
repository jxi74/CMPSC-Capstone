using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
// System.Ling is used for finding the IDataPersistence objects

// This class keeps track of current state of game's data
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName; //File name that can be set in inspector
    [SerializeField] private bool useEncryption; //Checkbox if you want the data encrypted or not in inspector

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    // One instance of this class, get publicly, but can only set privately in this class
    public static DataPersistenceManager instance { get; private set; }

    private void Awake() 
    {
        if (instance != null) 
        {
            //Throw error because by design there should only be one DPM
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start() 
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();
        
        // if no data can be loaded, initialize to a new game, TODO Might want to change this for multiple saves
        if (this.gameData == null) 
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it (If they need to be saved)
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() 
    {
        //Whenever player exits game, it will automatically save
        SaveGame();
    }

    // Used to find all scrips that have IDataPersistence defined in them
    private List<IDataPersistence> FindAllDataPersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}