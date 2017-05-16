using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameControl _thisObject;

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

    }
}