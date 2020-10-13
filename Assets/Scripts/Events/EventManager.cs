using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

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

    public void PlayerDeathEventInitialization()
    {
        PlayerDeathEventComplete();
    }

    public void PlayerDeathEventComplete()
    {
        GameControl.Instance.PlayerDeathEventsAreComplete();
    }
}