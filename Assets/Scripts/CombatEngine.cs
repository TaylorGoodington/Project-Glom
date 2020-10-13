using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using static Utility;

public class CombatEngine : MonoBehaviour
{
    public static CombatEngine Instance;

    private static Dictionary<int, EnemyStats> activeEnemies;
    private static Dictionary<int, SyphonInfo> syphonedStats;
    private static float initalSyphonTime = 1f;
    public static float syphonTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        ResetCombatEngine();
    }

    public void DealDamageToPlayer(int damage)
    {
        SaveDataController.Instance.playerCurrentHP = Mathf.Max(SaveDataController.Instance.playerCurrentHP - damage, 0);
        UserInterface.Instance.UpdateHealth();

        if (SaveDataController.Instance.playerCurrentHP == 0)
        {
            ResetCombatEngine();
            GameControl.Instance.PlayerHasDied();
        }
    }

    private void ResetCombatEngine()
    {
        StopCoroutine(TriggerSyphons());
        activeEnemies = new Dictionary<int, EnemyStats>();
        syphonTime = initalSyphonTime;
        syphonedStats = new Dictionary<int, SyphonInfo>();
    }

    public void ActivateEnemy(int id, EnemyStats stats)
    {
        activeEnemies.Add(id, stats);
        RefreshSyphons();
    }

    private void RefreshSyphons()
    {
        StopCoroutine(TriggerSyphons());
        StartCoroutine(TriggerSyphons());
    }

    private IEnumerator TriggerSyphons ()
    {
        while (activeEnemies.Count > 0)
        {
            yield return new WaitForSeconds(syphonTime);

            foreach (var enemy in activeEnemies)
            {
                if (enemy.Value.syphonType == EnemyStats.SyphonType.health)
                {
                    DealDamageToPlayer(enemy.Value.syphonAmount);
                }
                else
                {
                    GameControl.Instance.player.moveSpeed -= enemy.Value.syphonAmount;
                    GameControl.Instance.player.climbSpeed -= enemy.Value.syphonAmount;
                }

                UpdateSyphonnedStats(enemy.Key, enemy.Value.syphonType, enemy.Value.syphonAmount);
            }
        }
    }

    private void UpdateSyphonnedStats (int id, EnemyStats.SyphonType type, int amount)
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

    private void ReturnSyphonnedStats(int enemyId)
    {
        if (syphonedStats.ContainsKey(enemyId))
        {
            if (syphonedStats[enemyId].syphonType == EnemyStats.SyphonType.health)
            {
                DealDamageToPlayer(syphonedStats[enemyId].syphonedAmount * -1);
            }
            else
            {
                GameControl.Instance.player.moveSpeed += syphonedStats[enemyId].syphonedAmount;
                GameControl.Instance.player.climbSpeed += syphonedStats[enemyId].syphonedAmount;
            }

            syphonedStats.Remove(enemyId);
        }
    }

    public void EnemyDeath(int enemyId)
    {
        if (activeEnemies.ContainsKey(enemyId))
        {
            activeEnemies.Remove(enemyId);
        }

        ReturnSyphonnedStats(enemyId);
        RefreshSyphons();
    }

    public enum Attacker
    {
        Player,
        Enemy
    }
}

public class SyphonInfo
{
    public EnemyStats.SyphonType syphonType;
    public int syphonedAmount;

    public SyphonInfo (EnemyStats.SyphonType type, int amount)
    {
        syphonType = type;
        syphonedAmount = amount;
    }
}