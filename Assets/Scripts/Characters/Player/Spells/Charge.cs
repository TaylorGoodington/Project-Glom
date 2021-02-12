using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    float projectileBaseSpeed = 150f;
    float projectileSpeed = 150f;
    int projectileBaseDamage = 5;
    int projectileDamage = 3;
    float cooldown = 3f;
    float maxChargeTime = 4f;
    int castKey = 0;
    bool channeling = false;
    [SerializeField] GameObject projectile;

    //Casting is chargeable to a max of x seconds and does more damage proportionally
    //Charging goes through 3 phases which increase the damage and change the projectile animation
    public void Cast()
    {
        //tell the player animator that the cast has begun
        //start ienumerator to complete the cast unless it is cancelled early -> ienumerator is just the countdown
        //Check for and apply previous charge bounses
        if (channeling == false)
        {
            UpdateProperties();
            castKey++;
            StartCoroutine(ChargeTimer());
            channeling = true;
        }
        else
        {
            Release();
        }
    }

    public void Release()
    {
        //releases the projectile
        StopAllCoroutines();
        channeling = false;
        GameObject blast = Instantiate(projectile, GameControl.Instance.player.transform.position, Quaternion.identity);
        blast.GetComponent<ProjectileMovement>().Initialize(projectileSpeed, projectileDamage, castKey, GameControl.Instance.player.FaceDirection()); 
        PlayerSpellControl.Instance.UpdateNextCastTime(cooldown);
        PlayerSpellControl.Instance.CastComplete();
    }

    private IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(maxChargeTime);
        Release();
    }

    private void UpdateProperties()
    {
        //ask how much damage was taken during charge

        //modify projectile speed and damage based on unlocked nodes and concurrence
        //return or record so projectiles can be updated
    }
}