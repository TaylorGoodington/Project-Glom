using UnityEngine;

public class AuraProjectile : MonoBehaviour
{
    float startRadius = 1f;
    float stageOneRadius = 8f;
    float stageTwoRadius = 11f;
    float stageThreeRadius = 15f;
    float stageFourRadius = 26f;
    float stageFiveRaidus = 42f;
    float stageSixRadius = 75f;
    float stageSevenRadius = 107f;
    float stageEightRadius = 160f;
    float timeBetweenStages = .167f;

    void Update()
    {
        transform.position = FindObjectOfType<Player>().transform.position;

        if (GetComponent<CircleCollider2D>().radius < stageOneRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageOneRadius - startRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageTwoRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageTwoRadius - stageOneRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageThreeRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageThreeRadius - stageTwoRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageFourRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageFourRadius - stageThreeRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageFiveRaidus)
        {
            GetComponent<CircleCollider2D>().radius += ((stageFiveRaidus - stageFourRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageSixRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageSixRadius - stageFiveRaidus) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageSevenRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageSevenRadius - stageSixRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius < stageEightRadius)
        {
            GetComponent<CircleCollider2D>().radius += ((stageEightRadius - stageSevenRadius) / timeBetweenStages) * Time.deltaTime;
        }
        else if (GetComponent<CircleCollider2D>().radius > 160)
        {
            Destroy(gameObject);
        }
    }
}
