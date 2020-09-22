using System.Collections;
using UnityEngine;
using static Utility;

public class Hazard : MonoBehaviour
{
    public float interval;
    public int damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameLayers.player)
        {
            StartCoroutine("DealDamage");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameLayers.player)
        {
            StopCoroutine("DealDamage");
        }
    }

    private IEnumerator DealDamage()
    {
        while (true)
        {
            CombatEngine.Instance.DealDamageToPlayer(damage);
            yield return new WaitForSeconds(interval);
        }
    }
}