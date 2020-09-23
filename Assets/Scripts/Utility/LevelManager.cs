using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadIntroduction()
    {
        SceneManager.LoadScene("Company Logo");
    }

    public void LoadLevelZero()
    {
        SceneManager.LoadScene("Level Zero");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level One");
    }
}