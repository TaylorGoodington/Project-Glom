using UnityEngine;
using static Utility;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public InputState inputState;
    public Player player;

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

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        inputState = InputState.None;
        GameData.Instance.InitializeGameData();
        inputState = InputState.Player_Character;
    }
}