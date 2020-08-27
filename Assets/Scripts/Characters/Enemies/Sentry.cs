using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class Sentry : EnemyBase
{
    public override void Start()
    {
        base.Start();
        enemyAnimationController.Play("Inactive");
    }

    void Update()
    {
        EnemyUpdate();
    }

    public void SetActiveStatus()
    {
        enemyAnimationController.SetBool("isActive", true);
        ActivateEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemyAnimationController.Play("Target Aquired");
        }
    }
}
