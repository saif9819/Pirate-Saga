using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highScores;
    public int[] stars;
}

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Load();
    }
    void Start()
    {
        
    }

    public void Save()
    {
        //Binary Formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Route from the program to the file

        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);

        //create a copy save data
        SaveData data = new SaveData();
        data = saveData;
        
        //Actually save the data in the file
        formatter.Serialize(file, data);
        
        //Close the data stream
        file.Close();
        Debug.Log("Saved ");
    }
    
    public void Load()
    {
        //if the save game file exits
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            //create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData=formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Loaded ");

        }
        else
        {
            saveData=new SaveData();
            saveData.isActive = new bool[100];
            saveData.stars = new int[100];
            saveData.highScores = new int[100];
            saveData.isActive[0] = true;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
