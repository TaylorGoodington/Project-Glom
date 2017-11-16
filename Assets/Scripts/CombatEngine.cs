using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatEngine : MonoBehaviour
{
    private static Dictionary<int, EnemyStats> activeEnemies;
    private static Dictionary<int, SyphonInfo> syphonedStats;
    private static float initalSyphonTime = 2f;
    public static float syphonTime;
    public static CombatEngine instance;
    private static Player player;
    //public int attackDamage;
    //public float critRate;
    //[HideInInspector] public int maxCombos;
    //public int comboCount;
    //public bool runComboClock;
    //public float comboWindow;
    //public float comboCountDown;
    //public int enemyFaceDirection;
    //public int enemyKnockBackForce;
    //public int enemyKnockBackDirection;
    //public Skills damagingAbility;

    //private float maxIntelligence = 10000; //ToDo UPDATE AT SOME POINT
    //private float maxNakedCritRate = 25;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        activeEnemies = new Dictionary<int, EnemyStats>();
        syphonTime = initalSyphonTime;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        syphonedStats = new Dictionary<int, SyphonInfo>();

        //comboCount = 1;
        //runComboClock = false;
        //comboWindow = 0.25f;
        //comboCountDown = comboWindow;
    }

    void Update()
    {
        //maxCombos = GameControl.gameControl.maxCombos;
        //if (runComboClock)
        //{
        //    RunComboClock();
        //}
    }

    public static void ActivateEnemy(int id, EnemyStats stats)
    {
        activeEnemies.Add(id, stats);
        instance.StopCoroutine("TriggerSyphons");
        instance.StartCoroutine("TriggerSyphons");
    }

    private IEnumerator TriggerSyphons ()
    {
        while (true)
        {
            foreach (var enemy in activeEnemies)
            {
                if (enemy.Value.syphonType == EnemyStats.SyphonType.health)
                {
                    GameControl.playerCurrentHP -= enemy.Value.syphonAmount;

                }
                else
                {
                    player.moveSpeed -= enemy.Value.syphonAmount;
                    player.climbSpeed -= enemy.Value.syphonAmount;
                }

                UpdateSyphonnedStats(enemy.Key, enemy.Value.syphonType, enemy.Value.syphonAmount);
            }

            UserInterface.UpdateHealth();
            yield return new WaitForSeconds(syphonTime);
        }
    }

    private void UpdateSyphonnedStats (int id, EnemyStats.SyphonType type, float amount)
    {
        if (syphonedStats.ContainsKey(id))
        {
            syphonedStats[id].syphonedAmount += amount;
        }
        else
        {
            syphonedStats.Add(id, new SyphonInfo(type, amount));
        }
    }

    //public void RunComboClock()
    //{
    //    if (comboCountDown > 0)
    //    {
    //        comboCountDown -= Time.deltaTime;
    //    }
    //    else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().attackLaunched == false)
    //    {
    //        runComboClock = false;
    //        comboCountDown = comboWindow;
    //        comboCount = 1;
    //    }
    //}

    //public void CalculateSecondaryStats() {
    //    float critModifier = 0;

    //    foreach (Skills skill in SkillsController.skillsController.activeSkills) {
    //        critModifier += skill.critModifier;
    //    }
    //    critRate = ((GameControl.gameControl.currentIntelligence / maxIntelligence) * maxNakedCritRate) + (critModifier * 100);
    //}

    //public void CalculateAttackDamage() {
    //    Equipment.EquipmentType weaponClass = EquipmentDatabase.equipmentDatabase.equipment[GameControl.gameControl.profile1Weapon].equipmentType;
    //    if (weaponClass == Equipment.EquipmentType.Sword || weaponClass == Equipment.EquipmentType.Axe)
    //    {
    //        attackDamage = GameControl.gameControl.currentStrength;
    //    }
    //    else if (weaponClass == Equipment.EquipmentType.Polearm || weaponClass == Equipment.EquipmentType.Knuckle)
    //    {
    //        attackDamage = GameControl.gameControl.currentStrength;
    //    }
    //    else if (weaponClass == Equipment.EquipmentType.Talisman || weaponClass == Equipment.EquipmentType.Dagger)
    //    {
    //        attackDamage = GameControl.gameControl.currentStrength;
    //    }
    //    else if (weaponClass == Equipment.EquipmentType.Bow || weaponClass == Equipment.EquipmentType.Staff)
    //    {
    //        attackDamage = GameControl.gameControl.currentIntelligence;
    //    }
    //}

    //public bool AttackingPhase(Collider2D collider, Attacker attacker) {
    //    if (attacker == Attacker.Player)
    //    {
    //        SkillsController.skillsController.ActivatePlayerAbilities(Skills.TriggerPhase.Attacking);
    //        SkillsController.skillsController.ActivateEnemyAbilities(Skills.TriggerPhase.BeingAttacked, collider);
    //    }
    //    else
    //    {
    //        SkillsController.skillsController.ActivatePlayerAbilities(Skills.TriggerPhase.BeingAttacked);
    //        SkillsController.skillsController.ActivateEnemyAbilities(Skills.TriggerPhase.Attacking, collider);
    //    }
    //    return true;
    //}

    //public bool HittingPhase(Collider2D collider, Attacker attacker)
    //{
    //    if (attacker == Attacker.Player)
    //    {
    //        SkillsController.skillsController.ActivatePlayerAbilities(Skills.TriggerPhase.Hitting);
    //        SkillsController.skillsController.ActivateEnemyAbilities(Skills.TriggerPhase.BeingHit, collider);
    //    }
    //    else
    //    {
    //        SkillsController.skillsController.ActivatePlayerAbilities(Skills.TriggerPhase.BeingHit);
    //        SkillsController.skillsController.ActivateEnemyAbilities(Skills.TriggerPhase.Hitting, collider);
    //    }
    //    return true;
    //}

    //public bool DealingDamageToEnemyPhase(Collider2D collider, bool damageFromAbility = false)
    //{
    //    int enemyDefense = collider.gameObject.GetComponent<EnemyStats>().defense;

    //    SkillsController.skillsController.ActivatePlayerAbilities(Skills.TriggerPhase.DealingDamage);
    //    SkillsController.skillsController.ActivateEnemyAbilities(Skills.TriggerPhase.BeingDamaged, collider);

    //    CalculateAttackDamage();
    //    CalculateSecondaryStats();

    //    if (damageFromAbility)
    //    {
    //        //TODO change attack damage based on skill used...
    //        Skills.RequiredStat damageStat = damagingAbility.requiredStatName;
    //        if (damageStat == Skills.RequiredStat.Intelligence)
    //        {
    //            attackDamage = GameControl.gameControl.currentIntelligence;
    //        }
    //        else if (damageStat == Skills.RequiredStat.Strength)
    //        {
    //            attackDamage = GameControl.gameControl.currentStrength;
    //        }
    //        else if (damageStat == Skills.RequiredStat.Defense)
    //        {
    //            attackDamage = GameControl.gameControl.currentDefense;
    //        }
    //        else if (damageStat == Skills.RequiredStat.Agility)
    //        {
    //            attackDamage = GameControl.gameControl.currentSpeed;
    //        }
    //        else
    //        {
    //            //no modification.
    //        }
    //        attackDamage = Mathf.RoundToInt(attackDamage * damagingAbility.damageDelt);
    //    }


    //    ////Check for a crit.
    //    //float randomNumber = Random.Range(0, 100);
    //    //if (randomNumber <= critRate)
    //    //{
    //    //    attackDamage = attackDamage * 2;
    //    //}

    //    if ((attackDamage - enemyDefense) > 0)
    //    {
    //        collider.gameObject.GetComponent<EnemyStats>().hP -= (attackDamage - enemyDefense);
    //        collider.GetComponent<EnemyBase>().DisplayDamageReceived(attackDamage);
    //        collider.GetComponent<EnemyBase>().BeingAttacked();
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool DealingDamageToPlayerPhase(Collider2D collider, int damage)
    //{
    //    int playerDefense = GameControl.gameControl.currentDefense;
    //    int enemyAttackDamage = damage;

    //    SkillsController.skillsController.ActivatePlayerAbilities(Skills.TriggerPhase.BeingDamaged);
    //    SkillsController.skillsController.ActivateEnemyAbilities(Skills.TriggerPhase.DealingDamage, collider);

    //    if ((enemyAttackDamage - playerDefense) > 0)
    //    {
    //        GameControl.gameControl.hp -= (enemyAttackDamage - playerDefense);
    //        return true;
    //    }
    //    else
    //    {
    //        GameControl.gameControl.hp -= collider.GetComponent<EnemyStats>().minimumDamage;
    //        return true;
    //    }
    //}

    //public IEnumerator AttackingEnemy(Collider2D collider, bool damageFromAbility = false)
    //{
    //    if (AttackingPhase(collider, Attacker.Player))
    //    {
    //        if (HittingPhase(collider, Attacker.Player))
    //        {
    //            if (DealingDamageToEnemyPhase(collider, damageFromAbility))
    //            {

    //            }
    //        }
    //    }
    //    else
    //    {
    //        //at the end we should clear the active skills list for anything the triggered, that doesnt have a duration.
    //        SkillsController.skillsController.ClearAttackingCombatTriggeredAbilitiesFromList();
    //    }
    //    yield return null;
    //}

    ////The collider being passed here is actually the enemy attacker, since we would have no other way of knowing which one it was.
    //public void AttackingPlayer (Collider2D collider, int damage)
    //{
    //    if (AttackingPhase(collider, Attacker.Enemy))
    //    {
    //        if (HittingPhase(collider, Attacker.Enemy))
    //        {
    //            if (DealingDamageToPlayerPhase(collider, damage))
    //            {
    //                if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().uninterupatble)
    //                {
    //                    //Hazard Response.
    //                    if (collider.GetComponent<Hazard>())
    //                    {
    //                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().flinching = true;
    //                        collider.GetComponent<Hazard>().SendMessage("KnockBack");
    //                    }

    //                    //Conventional Enemy.
    //                    else
    //                    {
    //                        enemyFaceDirection = collider.GetComponent<Controller2D>().collisions.faceDir;
    //                        enemyKnockBackForce = collider.GetComponent<EnemyStats>().knockbackForce;
    //                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().flinching = true;
    //                        GameObject.FindGameObjectWithTag("Player").SendMessage("Knockback");
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    public enum Attacker
    {
        Player,
        Enemy
    }
}

public class SyphonInfo
{
    public EnemyStats.SyphonType syphonType;
    public float syphonedAmount;

    public SyphonInfo (EnemyStats.SyphonType type, float amount)
    {
        syphonType = type;
        syphonedAmount = amount;
    }
}