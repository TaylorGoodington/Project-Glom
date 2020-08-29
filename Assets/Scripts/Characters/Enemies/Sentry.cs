using UnityEngine;

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
        EnemyUpdate();

        if (mindSet == Mindset.Dying)
        {
            Breaking();
        }
    }

    private void Breaking()
    {
        breakingAnimation = UnityEngine.Random.Range(1, 5);
        enemyAnimationController.Play("Breaking " + breakingAnimation);
        mindSet = Mindset.Dead;
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