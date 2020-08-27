using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int currentHp;
    public int maxHP;
    public int pointsGranted;
    public SyphonType syphonType;
    public int syphonAmount;
    public int attackDamage;
    public int attackRange;
    public float jumpHeight;
    public float patrolSpeed;
    public float chaseSpeed;
    [Tooltip("The amount of time an enemy will remain engaged after losing the line of sight.")]
    public float chaseTime;
    [Tooltip("The length of time in seconds it takes for the enemy to change directions.")]
    public float pivotTime;
    public float maxRageTime;

    public enum SyphonType
    {
        health,
        speed
    }

    void Start ()
    {
        maxHP *= GameControl.difficulty;
        currentHp = maxHP;
        currentHp *= GameControl.difficulty;
        attackDamage *= GameControl.difficulty;
        syphonAmount *= GameControl.difficulty;
    }
}