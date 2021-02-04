using UnityEngine;
using static Utility;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public InputState inputState;
    public Player player;
    public Camera mainCamera;
    public int selectedSpellId;
    public int player_CurrentHP;

    public OffensiveSpell currentOffensiveSpell;
    public OffensiveSpellVariant currentOffensiveSpellVariant;

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
        inputState = InputState.None;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        SetPlayerHealthOnLevelInitialization();
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
        player_CurrentHP = 0;
        inputState = InputState.None;
        MusicController.Instance.PlayTrack(Utility.MusicTrack.Player_Death);
        SoundEffectsController.Instance.PlaySoundEffect(Utility.SoundEffect.Player_Death);
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
        SetPlayerHealthOnLevelInitialization();
    }

    private void SetPlayerHealthOnLevelInitialization()
    {
        player_CurrentHP = SaveDataController.Instance.player_MaxHP;
    }
    #endregion

    public void ProcessEndOfTowerCurrency(float currency)
    {
        //for now??
        SaveDataController.Instance.currency += Mathf.CeilToInt(currency);
    }
}