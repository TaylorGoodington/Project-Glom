using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class Helmet : EnemyBase
{
    public float detectionWidthOffset = 40;
    public float detectionHeightOffset = 10;

    public override void Start()
    {
        base.Start();
        InitializePlayerDetection();
        InitializeDetectionPoints();
    }

    void Update()
    {
        EnemyUpdate();
    }

    void InitializeDetectionPoints()
    {
        Vector2 firstPoint = new Vector2(-1 * detectionWidthOffset, detectionHeightOffset);
        Vector2 secondPoint = new Vector2(detectionWidthOffset, detectionHeightOffset);
        Vector2 thirdPoint = new Vector2(detectionWidthOffset, -1 * detectionHeightOffset);
        Vector2 fourthPoint = new Vector2(-1 * detectionWidthOffset, -1 * detectionHeightOffset);

        Vector2[] detectionPoints = {firstPoint, secondPoint,thirdPoint, fourthPoint};
        playerDetection.GetComponent<PolygonCollider2D>().points = detectionPoints;
    }

    public override void AdjustMindset()
    {
        if (playerDetection != null)
        {
            if (playerDetection.isPlayerDetected && mindSet == EnemyMindset.Patroling)
            {
                mindSet = EnemyMindset.Attack_Prep;
            }
            else if (!playerDetection.isPlayerDetected && mindSet == EnemyMindset.Attacking)
            {
                mindSet = EnemyMindset.Attack_Recovery;
            }
        }
    }

    public override void Standing()
    {
        enemyAnimationController.Play("Helmet_Standing");
    }

    public override void Patrolling()
    {
        base.Patrolling();
        enemyAnimationController.Play("Helmet_Patroling");
    }

    public override void Attacking()
    {
        velocity.x = 0;
    }

    public override void Dying()
    {
        enemyAnimationController.Play("Helmet_Standing");
    }
}