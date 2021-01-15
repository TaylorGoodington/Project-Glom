using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    //nodes that have been upgraded
    //spell behavior based on those upgrades
    //projectiles or effects that are needed

    float projectileBaseSpeed = 1f;
    int projectileDamage = 3;
    int projectileBaseDamage = 5;
    float projectileSpeed = 1f;
    float cooldown = 0f;
    int concurrenceCounter = 0;
    int concurrenceTarget = 0;
    int concurrenctCastKey = 0;
    int castKey = 0;
    float timeBetweenProjectiles = 0.3f;
    bool applyConcurrence = false;
    [SerializeField] GameObject projectile;

    public void ApplyUnlockedNodes()
    {

    }

    public void Cast()
    {
        UpdateProperties();
        castKey++;
        StartCoroutine(Instantiate());
        //instantiate projectiles as per the burst way
        //after projectile instantiation i need to set the castKey on the projectile script

    }

    IEnumerator Instantiate ()
    {
        GameObject blast = Instantiate(projectile, GameControl.Instance.player.transform.position, Quaternion.identity);
        blast.GetComponent<ProjectileMovement>().Initialize(projectileSpeed, projectileDamage, castKey);
        yield return new WaitForSeconds(timeBetweenProjectiles);
        blast = Instantiate(projectile, GameControl.Instance.player.transform.position, Quaternion.identity);
        blast.GetComponent<ProjectileMovement>().Initialize(projectileSpeed, projectileDamage, castKey);
        yield return new WaitForSeconds(timeBetweenProjectiles);
        blast = Instantiate(projectile, GameControl.Instance.player.transform.position, Quaternion.identity);
        blast.GetComponent<ProjectileMovement>().Initialize(projectileSpeed, projectileDamage, castKey);
    }

    private void UpdateProperties()
    {
        //check for concurrence
        if (applyConcurrence)
        {
            applyConcurrence = false;
        }
        
        //modify projectile speed and damage based on unlocked nodes and concurrence
        //return or record so projectiles can be updated
    }

    public void ProcessHitInformation(int castKey, int enemyObjectId)
    {
        ConcurrenceCheck(castKey, enemyObjectId);
    }

    public void ConcurrenceCheck(int castKey, int enemyObjectId)
    {
        if (castKey == concurrenctCastKey && enemyObjectId == concurrenceTarget)
        {
            if (concurrenceCounter == 1)
            {
                concurrenceCounter++;
            }
            //Concurrence Achieved
            else
            {
                applyConcurrence = true;
                concurrenceCounter = 0;
                concurrenctCastKey = 0;
                concurrenceTarget = 0;
            }
        }
        else
        {
            concurrenceCounter = 1;
            concurrenctCastKey = castKey;
            concurrenceTarget = enemyObjectId;
        }
    }
}