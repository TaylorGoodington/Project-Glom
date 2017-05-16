using UnityEngine;
using System.Collections;

public class DamageDisplayTimer : MonoBehaviour
{
    float timer = 1;

	void Update () {
        timer -= Time.deltaTime;
        transform.Translate(new Vector3(0, 1) * Time.deltaTime * 25);
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
