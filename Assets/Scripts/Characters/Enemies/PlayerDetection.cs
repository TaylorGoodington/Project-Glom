using UnityEngine;
using static Utility;

public class PlayerDetection : MonoBehaviour
{
    public bool isPlayerDetected;

    void Start()
    {
        isPlayerDetected = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameLayers.player)
        {
            isPlayerDetected = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameLayers.player)
        {
            isPlayerDetected = false;
        }
    }
}