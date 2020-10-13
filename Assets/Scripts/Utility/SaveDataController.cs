using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static Utility;

public class SaveDataController : MonoBehaviour
{
    public static SaveDataController Instance { get; set; }

    public bool isFirstExecution;
    public int playerCurrentHP;
    public int currency;
    public int currentLevel;
    public int difficulty;
    public int activeSpellId;
    public List<int> availableSpellIds;

    #region Upgrades
    public int healthLevel;
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ReadSaveData();
    }

    public void InitializeGameData()
    {
        ReadSaveData();
    }

    private void ReadSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/Game_Data.sav"))
        {
            FileStream saveFile = File.Open(Application.persistentDataPath + "/Game_Data.sav", FileMode.Open);
            using (saveFile)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                SaveData saveData = (SaveData)binaryFormatter.Deserialize(saveFile);

                isFirstExecution = saveData.isFirstExecution;
                currency = saveData.currency;
            }
        }
        else
        {
            //First Execution
            BuildInitialSaveData();
        }
    }

    private void WriteSaveData()
    {
        //UpdateGameData();

        FileStream saveFile = File.Create(Application.persistentDataPath + "/Game_Data.sav");
        using (saveFile)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SaveData saveData = new SaveData();

            saveData.isFirstExecution = isFirstExecution;
            saveData.currency = currency;

            binaryFormatter.Serialize(saveFile, saveData);
        }
    }

    private void BuildInitialSaveData()
    {
        isFirstExecution = true;
        currency = 0;

        #region For Testting
        playerCurrentHP = 36;
        currentLevel = 1;
        healthLevel = 1;
        activeSpellId = 0;
        difficulty = 1;
        availableSpellIds = new List<int>();
        availableSpellIds.Add(0);
        availableSpellIds.Add(1);
        availableSpellIds.Add(2);
        #endregion
    }
}

[Serializable]
struct SaveData
{
    public bool isFirstExecution;
    public int currency;
}