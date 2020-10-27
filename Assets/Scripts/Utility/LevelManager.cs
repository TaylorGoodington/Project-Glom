using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using static Utility;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

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

    public void LoadLevel(Levels level)
    {
        SceneManager.LoadScene(level.ToString());
        SaveDataController.Instance.WriteSaveData();
    }

    public void EnterTheTower()
    {
        StartCoroutine(LoadTower());
    }

    public void LeaveTheTower()
    {
        StartCoroutine(LeaveTower());
    }

    private IEnumerator LoadTower()
    {
        yield return new WaitForSeconds(SceneTransitions.Instance.TransitionOut());

        LoadLevel(Levels.Level_One);
    }

    private IEnumerator LeaveTower()
    {
        yield return new WaitForSeconds(SceneTransitions.Instance.TransitionOut());

        LoadLevel(Levels.Level_Zero);
    }
}