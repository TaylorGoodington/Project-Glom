using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            LevelManager.Instance.EnterTheTower();
        }
    }
}