using System.Collections;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float interval;
    public int damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameControl.GameLayers.player)
        {
            StartCoroutine("DealDamage");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)GameControl.GameLayers.player)
        {
            StopCoroutine("DealDamage");
        }
    }

    private IEnumerator DealDamage()
    {
        while (true)
        {
            GameControl.DealDamageToPlayer(damage);
            yield return new WaitForSeconds(interval);
        }
    }
}