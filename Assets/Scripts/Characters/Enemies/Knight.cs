using UnityEngine;
using static Utility;

public class Knight : EnemyBase
{
    [SerializeField] float detectionWidthOffset = 40;
    [SerializeField] float detectionHeightOffset = 10;
    [SerializeField] GameObject weaponPrefabLeft = null;
    [SerializeField] GameObject weaponPrefabRight = null;

    private GameObject weapon = null;

    public override void Start()
    {
        base.Start();
        InitializePlayerDetection();
        InitializeDetectionPoints();
    }

    void Update()
    {
        EnemyUpdate();

        if (GetComponent<SpriteRenderer>().flipX)
        {
            GetComponentInChildren<PolygonCollider2D>().offset = new Vector2(detectionWidthOffset, 0);
        }
        else
        {
            GetComponentInChildren<PolygonCollider2D>().offset = Vector2.zero;
        }
    }

    void InitializeDetectionPoints()
    {
        Vector2 firstPoint = new Vector2(-1 * detectionWidthOffset, detectionHeightOffset);
        Vector2 secondPoint = new Vector2(0, detectionHeightOffset);
        Vector2 thirdPoint = new Vector2(0, -1 * detectionHeightOffset);
        Vector2 fourthPoint = new Vector2(-1 * detectionWidthOffset, -1 * detectionHeightOffset);

        Vector2[] detectionPoints = { firstPoint, secondPoint, thirdPoint, fourthPoint };
        playerDetection.GetComponent<PolygonCollider2D>().points = detectionPoints;
    }

    public override void AdjustMindset()
    {
        if (playerDetection != null)
        {
            if (playerDetection.isPlayerDetected && mindSet == EnemyMindsets.Patroling)
            {
                mindSet = EnemyMindsets.Attacking;
            }
        }
    }

    public override void Standing()
    {
        enemyAnimationController.Play("Knight_Standing");
    }

    public override void Patrolling()
    {
        base.Patrolling();
        enemyAnimationController.Play("Knight_Patroling");
    }

    public override void Attacking()
    {
        velocity.x = 0;
        enemyAnimationController.Play("Knight_Attacking");
    }

    public void LaunchProjectile()
    {
        if (GetComponent<SpriteRenderer>().flipX)
        {
            weapon = Instantiate(weaponPrefabRight, transform.position, Quaternion.identity);
        }
        else
        {
            weapon = Instantiate(weaponPrefabLeft, transform.position, Quaternion.identity);
        }
    }

    public void AttackComplete()
    {
        mindSet = EnemyMindsets.Patroling;
        Destroy(weapon);
    }

    public override void Dying()
    {
        enemyAnimationController.Play("Knight_Standing");
    }
}