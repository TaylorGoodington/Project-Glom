using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : EnemyBase
{
    public override void Start()
    {
        base.Start();
        enemyAnimationController.Play("Inactive");
    }

    void Update()
    {
        EnemyUpdate();

        if (mindSet == Mindset.Dying)
        {
            //Breaking();
        }
    }
}