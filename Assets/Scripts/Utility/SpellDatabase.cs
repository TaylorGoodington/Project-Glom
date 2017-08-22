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
        spells.Add(new Spells(0, "main", 5, 0, 3));

        spellProjectiles = new List<GameObject>();
        spellProjectiles = initalSpellProjectiles;
        initalSpellProjectiles = new List<GameObject>();
	}
}

public class Spells
{
    public int id;
    public string name;
    public float baseDamage;
    public float cooldown;
    public float projectileSpeed;

    public Spells (int spellId, string spellName, float spellBaseDamage, float spellCooldown, float speed)
    {
        id = spellId;
        name = spellName;
        baseDamage = spellBaseDamage;
        cooldown = spellCooldown;
        projectileSpeed = speed;
    }
}