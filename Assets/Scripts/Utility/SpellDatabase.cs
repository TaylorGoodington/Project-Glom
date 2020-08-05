using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour
{
    public static List<Spells> spells;
    public List<GameObject> initalSpellProjectiles;
    public static List<GameObject> spellProjectiles;

	void Start ()
    {
        spells = new List<Spells>();
        spells.Add(new Spells(0, "Blast", true, 5, 0, 3));
        spells.Add(new Spells(1, "Aura", false, 5, 2, 0));
        spells.Add(new Spells(2, "Poof", false, 0, 3, 0));

        spellProjectiles = new List<GameObject>();
        spellProjectiles = initalSpellProjectiles;
        initalSpellProjectiles = new List<GameObject>();
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