using UnityEngine;

public class DamageReceived : MonoBehaviour
{
    public Sprite[] numbers;
    public GameObject oneDigitNumber;
    public GameObject twoDigitNumber;
    public GameObject threeDigitNumber;
    public GameObject fourDigitNumber;

    void Start()
    {
        BoxCollider2D enemy = transform.parent.GetComponent<BoxCollider2D>();
        transform.position = new Vector2(enemy.bounds.center.x, enemy.bounds.max.y + 5);
    }

    public void CalculateTheNumber(int damage)
    {
        //Single Digit
        if (damage < 10)
        {
            GameObject display = Instantiate(oneDigitNumber, transform.position, Quaternion.identity) as GameObject;
            display.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = numbers[damage];
        }
        //Double Digit 
        else if (damage < 100)
        {
            int onesPlace = damage % 10;
            int tensPlace = Mathf.FloorToInt(damage / 10);
            GameObject display = Instantiate(twoDigitNumber, transform.position, Quaternion.identity) as GameObject;
            display.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = numbers[onesPlace];
            display.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = numbers[tensPlace];
        }
        //Triple Digit
        else if (damage < 1000)
        {
            int onesPlace = (damage % 100) % 10;
            int tensPlace = Mathf.FloorToInt(damage / 10) % 10;
            int hundredsPlace = Mathf.FloorToInt(damage / 100);
            GameObject display = Instantiate(threeDigitNumber, transform.position, Quaternion.identity) as GameObject;
            display.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = numbers[onesPlace];
            display.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = numbers[tensPlace];
            display.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = numbers[hundredsPlace];
        }
        //Quadruple Digit
        else
        {
            int onesPlace = ((damage % 1000) % 100) % 10;
            int tensPlace = (Mathf.FloorToInt(damage / 10) % 100) % 10;
            int hundredsPlace = Mathf.FloorToInt(damage / 100) % 10;
            int thousandsPlace = Mathf.FloorToInt(damage / 1000);
            GameObject display = Instantiate(fourDigitNumber, transform.position, Quaternion.identity) as GameObject;
            display.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = numbers[onesPlace];
            display.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = numbers[tensPlace];
            display.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = numbers[hundredsPlace];
            display.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = numbers[thousandsPlace];
        }
    }
}