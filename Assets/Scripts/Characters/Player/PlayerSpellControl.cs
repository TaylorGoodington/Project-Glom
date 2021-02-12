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
    private Charge charge = null;
    private float nextCastTime = 0;

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

            burst = GetComponent<Burst>();
            charge = GetComponent<Charge>();

            spells = new List<Spells>();
            spells.Add(new Spells(0, "Blast", true, 5, 0, 1));
            spells.Add(new Spells(1, "Aura", false, 5, 2, 0));
            spells.Add(new Spells(2, "Poof", false, 0, 3, 0));
        }

        nextCastTime = Time.time;
    }

    //called from the player depending on input and state
    public bool CanCast()
    {
        if (Time.time > nextCastTime)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public GameObject ReturnSpellProjectile (int spellId)
    {
        return spellProjectiles[spellId];
    }

    public void CastComplete()
    {
        //update cooldown list
        GameControl.Instance.player.CastComplete();
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
            else if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.Charge)
            {
                charge.Cast();
            }
        }
    }

    public void UpdateNextCastTime(float cooldown)
    {
        nextCastTime = Time.time + cooldown;
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
            else if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.Charge)
            {
                //burst.ProcessHitInformation(castKey, enemyObjectId);
            }
        }
    }

    public CastingState RetrieveCastingState()
    {
        if (GameControl.Instance.currentOffensiveSpell == OffensiveSpell.Blast)
        {
            if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.None)
            {
                return CastingState.Instant;
            }
            else if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.Burst)
            {
                return CastingState.Instant;
            }
            else if (GameControl.Instance.currentOffensiveSpellVariant == OffensiveSpellVariant.Charge)
            {
                return CastingState.Channel;
            }
        }

        return CastingState.Instant;
    }

    //public void UpdateCoolDownList()
    //{
    //    for (int i = 0; i < cooldownList.Count; i++)
    //    {
    //        var key = cooldownList.ElementAt(i);
    //        int itemKey = key.Key;
    //        if (cooldownList[itemKey] <= 0)
    //        {
    //            cooldownList.Remove(itemKey);
    //        }
    //        else
    //        {
    //            cooldownList[itemKey] -= Time.deltaTime;
    //        }
    //    }
    //}

    //private void ExecuteSpellPoperties()
    //{
    //    cooldownList.Add(GameControl.Instance.selectedSpellId, PlayerSpellControl.Instance.spells[GameControl.Instance.selectedSpellId].cooldown);
    //    animator.PlaySpellAnimation(GameControl.Instance.selectedSpellId);

    //    //Blast
    //    if (GameControl.Instance.selectedSpellId == 0)
    //    {
    //        //move projectile instantiation from player animation controller.
    //    }

    //    //Aura
    //    else if (GameControl.Instance.selectedSpellId == 1)
    //    {

    //    }

    //    //Poof
    //    else if (GameControl.Instance.selectedSpellId == 2)
    //    {
    //        PoofSpell();
    //    }
    //}


    //private void PoofSpell()
    //{
    //    Vector2 input = InputController.Instance.CollectPlayerDirectionalInput();
    //    float closestDistance = 161;
    //    float rayLength = 160;
    //    Vector2 bottomLeft = new Vector2(GetComponent<Collider2D>().bounds.min.x, GetComponent<Collider2D>().bounds.min.y + .5f);
    //    Vector2 bottomRight = new Vector2(GetComponent<Collider2D>().bounds.max.x, GetComponent<Collider2D>().bounds.min.y + .5f);
    //    Vector2 topLeft = new Vector2(GetComponent<Collider2D>().bounds.min.x, GetComponent<Collider2D>().bounds.max.y - .5f);
    //    float horizontalRaySpacing = (GetComponent<Collider2D>().bounds.size.y - 1) / 3;
    //    float verticalRaySpacing = (GetComponent<Collider2D>().bounds.size.x - 1) / 3;

    //    if (input.y != 0)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Vector2 rayOrigin = (input.y == -1) ? bottomLeft : topLeft;
    //            rayOrigin += Vector2.right * (verticalRaySpacing * i + input.x);
    //            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * input.y, rayLength, 1 << 9);

    //            if (hit)
    //            {
    //                if (hit.distance < closestDistance)
    //                {
    //                    closestDistance = hit.distance;
    //                }
    //            }
    //        }

    //        float newPositionY;

    //        if (input.y == 1)
    //        {
    //            newPositionY = transform.position.y + closestDistance;
    //        }
    //        else
    //        {
    //            newPositionY = transform.position.y - closestDistance;
    //        }

    //        Vector2 poofDestination = new Vector2(transform.position.x, newPositionY);
    //        transform.position = poofDestination;
    //        UpdateHeightReached();
    //    }
    //    else 
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Vector2 rayOrigin = (faceDirection == -1) ? bottomLeft : bottomRight;
    //            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
    //            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * faceDirection, rayLength, controller.collisionMask);

    //            if (hit)
    //            {
    //                // this section is for moving through the side of platforms that can be fallen through.
    //                 if (hit.collider.tag == "Through")
    //                    {
    //                        if (input.y != 0)
    //                        {
    //                            continue;
    //                        }
    //                    }

    //                //this section is for moving through the side of platforms that can't be fallen through.
    //                if (hit.collider.tag == "3Sides")
    //                {
    //                    if (input.y != 0)
    //                    {
    //                        continue;
    //                    }
    //                }

    //                if (hit.distance == 0)
    //                {
    //                    continue;
    //                }

    //                if (hit.distance < closestDistance)
    //                {
    //                    closestDistance = hit.distance;
    //                }
    //            }
    //        }

    //        float newPositionX;

    //        if (faceDirection == 1)
    //        {
    //            newPositionX = transform.position.x + closestDistance;
    //        }
    //        else
    //        {
    //            newPositionX = transform.position.x - closestDistance;
    //        }

    //        Vector2 poofDestination = new Vector2(newPositionX, transform.position.y);
    //        transform.position = poofDestination;
    //    }
    //}
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