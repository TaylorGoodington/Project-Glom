using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour
{
    public static SpellDatabase Instance { get; set; }
    public static List<Spells> spells;
    public List<GameObject> spellProjectiles;

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
        spells = new List<Spells>();
        spells.Add(new Spells(0, "Blast", true, 5, 0, 3));
        spells.Add(new Spells(1, "Aura", false, 5, 2, 0));
        spells.Add(new Spells(2, "Poof", false, 0, 3, 0));
	}

    public GameObject ReturnSpellProjectile (int spellId)
    {
        return spellProjectiles[spellId];
    }
}

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