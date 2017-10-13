using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl _thisObject;

    public static bool playerHasControl;
    public static int playerCurrentHP;
    public static bool inMenus;
    public static int currentLevel;
    public static int difficulty;

    public static int selectedSpellId;

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
        playerCurrentHP = 10;
        currentLevel = 1;
        healthLevel = 1;
        selectedSpellId = 1;
    }
}