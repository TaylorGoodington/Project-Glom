using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl _thisObject;

    public static bool playerHasControl;
    public static int playerCurrentHP;
    public static bool inMenus;

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
    }
}