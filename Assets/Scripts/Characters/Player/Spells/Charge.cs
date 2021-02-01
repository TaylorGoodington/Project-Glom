using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    float projectileBaseSpeed = 1f;
    float projectileSpeed = 1f;
    int projectileBaseDamage = 5;
    int projectileDamage = 3;
    float cooldown = 0f;
    float maxChargeTime = 3f;

    [SerializeField] GameObject projectile;

    //Casting is chargeable to a max of x seconds and does more damage proportionally
    //Charging goes through 3 phases which increase the damage and change the projectile animation
    public void Cast()
    {
        //tell the player animator that the cast has begun
        //start ienumerator to complete the cast unless it is cancelled early -> ienumerator is just the countdown
        //Check for and apply previous charge bounses
        StartCoroutine(ChargeTimer());
    }

    public void Release()
    {
        //releases the projectile
    }

    private IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(maxChargeTime);
        Release();
    }
}