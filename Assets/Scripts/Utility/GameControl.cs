using UnityEngine;
using static Utility;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public InputStates inputState;
    public Player player;
    public Camera mainCamera;
    public int selectedSpellId;

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
        SaveDataController.Instance.InitializeGameData();
        inputState = InputStates.None;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void FindPlayer(Player player)
    {
        this.player = player;
    }

    public void MovePlayer(Vector2 destination)
    {
        player.transform.position = destination;
    }

    #region Player Death Cycle
    //Triggered by Combat Engine
    public void PlayerHasDied()
    {
        inputState = InputStates.None;
        MusicController.Instance.PlayTrack(MusicTracks.Player_Death);
        SoundEffectsController.Instance.PlaySoundEffect(SoundEffects.Player_Death);
        player.Die();
    }

    //Triggered by the end of the player's death animation
    public void PlayerDeathAnimationIsComplete()
    {
        EventManager.Instance.PlayerDeathEventInitialization();
    }

    public void PlayerDeathEventsAreComplete()
    {
        LevelManager.Instance.LeaveTheTower();
        SaveDataController.Instance.playerCurrentHP = 36;
    }
    #endregion

    public void ProcessEndOfTowerCurrency(float currency)
    {
        //for now??
        SaveDataController.Instance.currency += Mathf.CeilToInt(currency);
    }
}