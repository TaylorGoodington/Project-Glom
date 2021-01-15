using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class PlayerSpellControl : MonoBehaviour
{
    public static PlayerSpellControl Instance;

    public List<Spells> spells;
    public List<GameObject> spellProjectiles;

    private Burst burst = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Start ()
    {
        burst = GetComponent<Burst>();

        spells = new List<Spells>();
        spells.Add(new Spells(0, "Blast", true, 5, 0, 1));
        spells.Add(new Spells(1, "Aura", false, 5, 2, 0));
        spells.Add(new Spells(2, "Poof", false, 0, 3, 0));
	}

    public GameObject ReturnSpellProjectile (int spellId)
    {
        return spellProjectiles[spellId];
    }

    public void CastSpell()
    {
        if (GameControl.Instance.currentOffensiveSpell == OffensiveSpell.Blast)
        {
            if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.None)
            {

            }
            else if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.Burst)
            {
                burst.Cast();
            }
        }
    }

    //called from projectiles or effects on hit
    public void DisseminateHitInformation(int castKey, int enemyObjectId)
    {
        if (GameControl.Instance.currentOffensiveSpell == OffensiveSpell.Blast)
        {
            if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.None)
            {

            }
            else if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.Burst)
            {
                burst.ProcessHitInformation(castKey, enemyObjectId);
            }
        }
    }
}

[Serializable]
public class Spells
{
    public int id;
    public string name;
    public bool hasProjectile;
    public float baseDamage;
    public float cooldown;
    public float projectileSpeed;

    public Spells(int id, string name, bool hasProjectile, float baseDamage, float cooldown, float projectileSpeed)
    {
        this.id = id;
        this.name = name;
        this.hasProjectile = hasProjectile;
        this.baseDamage = baseDamage;
        this.cooldown = cooldown;
        this.projectileSpeed = projectileSpeed;
    }
}