using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceController : MonoBehaviour
{
    private int damage;
    private int castKey = 0;

    public void Initialize(int lanceDamage, int castKey, int direction)
    {
        this.damage = lanceDamage;
        this.castKey = castKey;

        if (direction < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
        {
            if (collision is BoxCollider2D && collision.gameObject.layer == 11)
            {
                collision.GetComponent<EnemyStats>().currentHp -= damage;
                PlayerSpellControl.Instance.DisseminateHitInformation(castKey, collision.gameObject.GetInstanceID());
            }
        }
    }

    public void CompleteCast()
    {

    }
}