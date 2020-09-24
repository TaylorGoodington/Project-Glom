using UnityEngine;
using static Utility;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public InputStates inputState;
    public Player player;
    public Camera mainCamera;

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
            DontDestroyOnLoad(Instance);
        }
    }

    void Start ()
    {
        GameData.Instance.InitializeGameData();
        inputState = InputStates.None;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        LevelManager.Instance.LoadIntroduction();
    }
}