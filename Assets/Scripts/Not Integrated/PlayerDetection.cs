using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour {

    public bool playerInRadius;

    void Start ()
    {
        playerInRadius = false;
    }

	public void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInRadius = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInRadius = false;
        }
    }
}
