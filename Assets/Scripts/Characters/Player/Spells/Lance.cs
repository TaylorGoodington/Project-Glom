using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour
{
    //nodes that have been upgraded
    //spell behavior based on those upgrades
    //projectiles or effects that are needed

    int lanceBaseDamage = 5;
    int lanceDamage = 3;

    float cooldown = 2f;
    int castKey = 0;

    int frenzyCounter = 0;
    int frenzyFactor;

    int concurrenceTarget = 0;
    int concurrenctCastKey = 0;

    [SerializeField] GameObject lancePrefab;

    public void ApplyUnlockedNodes()
    {

    }

    public void Cast()
    {
        UpdateProperties();
        castKey++;
        PlayerSpellControl.Instance.UpdateNextCastTime(cooldown);
        Instantiate();
    }

    private void Instantiate()
    {
        GameObject lance = Instantiate(lancePrefab, GameControl.Instance.player.transform.position, Quaternion.identity);
        lance.GetComponent<LanceController>().Initialize(lanceDamage, castKey, GameControl.Instance.player.FaceDirection());
        PlayerSpellControl.Instance.CastComplete();
    }

    private void UpdateProperties()
    {
        //check for concurrence
        
        //do something with the frenzy factor

        //modify projectile speed and damage based on unlocked nodes and concurrence
        //return or record so projectiles can be updated
    }

    public void ProcessHitInformation(int castKey, int enemyObjectId)
    {
        CalculateFrenzy();
    }

    void CalculateFrenzy()
    {
        //check the list of frenzy targets in the current cast key
        //add new enemys to the list to increase the frenzy
    }
}
