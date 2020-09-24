using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static Utility;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; set; }

    public GameStates levelZeroGameState;
    public GameStates levelOneGameState;
    public int playerCurrentHP;
    public int currentLevel;
    public int difficulty = 1;
    public int selectedSpellId;
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
        LoadSaveData();
    }

    public void InitializeGameData()
    {
        LoadSaveData();

        #region For Testting
        playerCurrentHP = 36;
        currentLevel = 1;
        healthLevel = 1;
        selectedSpellId = 0;
        difficulty = 1; 
        availableSpellIds = new List<int>();
        availableSpellIds.Add(0);
        availableSpellIds.Add(1);
        availableSpellIds.Add(2);
        #endregion
    }

    public void CycleSelectedSpell()
    {
        int currentListPosition = availableSpellIds.IndexOf(selectedSpellId);

        if (currentListPosition == (availableSpellIds.Count - 1))
        {
            selectedSpellId = availableSpellIds[0];
        }
        else
        {
            selectedSpellId = availableSpellIds[currentListPosition + 1];
        }
    }

    private void LoadSaveData()
    {
        if (File.Exists(Application.persistentDataPath + "/Game_Data.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/Game_Data.dat", FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(saveFile);

            levelZeroGameState = saveData.levelZeroGameState;

            saveFile.Close();
        }
        else
        {
            //First Execution
            levelZeroGameState = GameStates.First_Execution;
        }
    }

    private void SaveData()
    {
        //UpdateGameData();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/Game_Data.dat");
        SaveData saveData = new SaveData();

        saveData.levelZeroGameState = levelZeroGameState;

        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

}

[Serializable]
struct SaveData
{
    public GameStates levelZeroGameState;
}