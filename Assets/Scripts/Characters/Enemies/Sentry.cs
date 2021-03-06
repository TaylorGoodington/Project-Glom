﻿using UnityEngine;
using static Utility;

[RequireComponent(typeof(EnemyBase))]
public class Sentry : EnemyBase
{
    int breakingAnimation;

    public override void Start()
    {
        base.Start();
        enemyAnimationController.Play("Inactive");
    }

    void Update()
    {
        if (stats.currentHp <= 0 && mindSet != EnemyMindset.Dead)
        {
            CombatEngine.Instance.EnemyDeath(enemyId);
            mindSet = EnemyMindset.Dying;
        }

        if (mindSet == EnemyMindset.Dying)
        {
            Breaking();
        }
    }

    private void Breaking()
    {
        breakingAnimation = UnityEngine.Random.Range(1, 5);
        enemyAnimationController.Play("Breaking " + breakingAnimation);
        mindSet = EnemyMindset.Dead;
    }

    public void Broken()
    {
        enemyAnimationController.Play("Broken " + breakingAnimation);
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