using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl _thisObject;

    public static bool playerHasControl;
    public static int playerCurrentHP;
    public static bool inMenus;
    public static int currentLevel;
    public static int difficulty = 1;
    public static int selectedSpellId;

    public static Camera mainCamera;

    #region Upgrades
    public static int healthLevel;
    #endregion

    void Awake()
    {
        if (_thisObject != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _thisObject = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Start ()
    {
        playerHasControl = false;
        playerCurrentHP = 36;
        currentLevel = 1;
        healthLevel = 1;
        selectedSpellId = 1;
        difficulty = 1;
        mainCamera = GetComponentInChildren<Camera>();

        //TODO Remove later
        playerHasControl = true;
    }

    public static void DealDamageToPlayer(int damage)
    {
        playerCurrentHP -= damage;
        UserInterface.UpdateHealth();
    }

    public enum GameLayers
    {
        levelBounds = 8,
        ground = 9,
        platforms = 10,
        enemy = 11,
        player = 12,
        ladder = 13,
        ladderTop = 14,
        hazzard = 15
    }
}