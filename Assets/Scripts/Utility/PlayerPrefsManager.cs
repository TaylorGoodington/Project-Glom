using UnityEngine;
using System.Collections.Generic;

public class PlayerPrefsManager : MonoBehaviour
{

    const string MASTER_MUSIC_VOLUME_KEY = "master_volume";
    const string MASTER_EFFECTS_VOLUME_KEY = "master_effects";
    const string VALUE_LIST = "value_list_";

    public static void SetMasterMusicVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_MUSIC_VOLUME_KEY, volume);
        }
    }

    public static float GetMasterMusicVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_MUSIC_VOLUME_KEY);
    }

    public static void SetMasterEffectsVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_EFFECTS_VOLUME_KEY, volume);
        }
    }

    public static float GetMasterEffectsVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_EFFECTS_VOLUME_KEY);
    }

    public static void SetValueList (string name, bool inUse)
    {
        int tempBool;

        if (inUse == true)
        {
            tempBool = 1;
        }
        else
        {
            tempBool = 0;
        }
        
        PlayerPrefs.SetInt(VALUE_LIST + name, tempBool);
    }

    public static bool GetValueList (string name)
    {
        bool tempBool;

        if (PlayerPrefs.GetInt(VALUE_LIST + name) == 1)
        {
            tempBool = true;
        }
        else
        {
            tempBool = false;
        }

        return tempBool;
    }
}

public class MathNumbers
{
    public int number;
    public bool isInUse;

   public MathNumbers (int num, bool used)
    {
        number = num;
        isInUse = used;
    }
}