using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : EnemyBase
{
    public override void Start()
    {
        base.Start();
        //enemyAnimationController.Play("Inactive");
    }

    void Update()
    {
        EnemyUpdate();
    }

    public override void Patrolling()
    {
        base.Patrolling();
        enemyAnimationController.Play("Helmet_Patroling");
    }

    public override void AttackPrep()
    {
        velocity.x = 0;
        enemyAnimationController.Play("Helmet_Attack_Prep");
    }

    public override void Attacking()
    {
        velocity.x = 0;
    }

    public override void AttackRecovery()
    {
        velocity.x = 0;
        enemyAnimationController.Play("Helmet_Attack_Recovery");
    }

    public override void Dying()
    {
        
    }
}