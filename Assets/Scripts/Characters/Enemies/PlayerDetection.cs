using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool isPlayerDetected;
    private float detectionTime = 1f;
    private bool isActive;

    void Start()
    {
        isPlayerDetected = false;
    }

    //void Update()
    //{
    //    if (isPlayerDetected)
    //    {
    //        detectionTime -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        detectionTime = 1;
    //    }

    //    if (detectionTime <= 0)
    //    {
    //        if (!isActive)
    //        {
    //            //transform.parent.GetComponent<EnemyBase>().ActivateEnemy();
    //            isActive = true;
    //        }
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameControl.GameLayers.player)
        {
            isPlayerDetected = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameControl.GameLayers.player)
        {
            isPlayerDetected = false;
        }
    }
}