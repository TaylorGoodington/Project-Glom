using UnityEngine;
using System.Collections.Generic;

public class EnemyStats : MonoBehaviour {

    public int hP;
    public int maxHP;
    public int expGranted;
    public int maximumDamage;
    public int minimumDamage;
    public int defense;
    public int attackRange;
    //public List<Skills> acquiredSkillsList;
    //public List<Skills> activeSkillsList;
    //public List<Equipment> equipmentDropped;
    public float jumpHeight;
    public float patrolSpeed;
    public float chaseSpeed;
    public int knockbackForce;
    [Tooltip("The amount of time an enemy will remain engaged after losing the line of sight.")]
    public float chaseTime;
    [Tooltip("The length of time in seconds it takes for the enemy to change directions.")]
    public float pivotTime;
    [Tooltip("The length of time in seconds the enemy will need to build rage before it becomes enraged.")]
    public float maxRageTimer;
    [Tooltip("The length of time in seconds the enemy will be enraged. Set to Zero if the enemy does not Rage.")]
    public float maxEnragedTimer;

    //Add Plathrough modifier for the stats!!!
    void Start ()
    {

    }
}
