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
    public int player_MaxHP;
    public int currency;
    public int chosenDifficulty;
    public int activeSpellId;
    public List<int> availableSpellIds;

    #region Upgrades
    public int healthLevelUsedForSelectingUIHealthBar;
    int maxDifficulty;

    #region Offensive Spell Upgrades

    #region Burst

    #endregion

    #endregion

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
                player_MaxHP = saveData.player_MaxHP;
                chosenDifficulty = saveData.chosenDifficulty;
                maxDifficulty = saveData.maxDifficulty;
                activeSpellId = saveData.activeSpellId;
                availableSpellIds = saveData.availableSpellIds;
                healthLevelUsedForSelectingUIHealthBar = saveData.healthLevelUsedForSelectingUIHealthBar;

                #region Spell Data

                #endregion
            }
        }
        else
        {
            //First Execution
            BuildInitialSaveData();
        }
    }

    public void WriteSaveData()
    {
        //UpdateGameData();

        FileStream saveFile = File.Create(Application.persistentDataPath + "/Game_Data.sav");
        using (saveFile)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SaveData saveData = new SaveData();

            saveData.isFirstExecution = isFirstExecution;
            saveData.currency = currency;
            saveData.player_MaxHP = player_MaxHP;
            saveData.chosenDifficulty = chosenDifficulty;
            saveData.maxDifficulty = maxDifficulty;
            saveData.activeSpellId = activeSpellId;
            saveData.availableSpellIds = availableSpellIds;
            saveData.healthLevelUsedForSelectingUIHealthBar = healthLevelUsedForSelectingUIHealthBar;

            binaryFormatter.Serialize(saveFile, saveData);
        }
    }

    private void BuildInitialSaveData()
    {
        isFirstExecution = true;
        currency = 0;
        chosenDifficulty = 1;
        maxDifficulty = 1;

        #region For Testting
        player_MaxHP = 36;
        healthLevelUsedForSelectingUIHealthBar = 1;
        activeSpellId = 0;
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
    public int player_MaxHP;
    public int currency;
    public int chosenDifficulty;
    public int activeSpellId;
    public List<int> availableSpellIds;
    public int healthLevelUsedForSelectingUIHealthBar;
    public int maxDifficulty;
}