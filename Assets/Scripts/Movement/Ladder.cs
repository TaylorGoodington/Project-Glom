using UnityEngine;

public class Ladder : MonoBehaviour
{
    private BoxCollider2D ladder;
    private BoxCollider2D top;
    private int sizeAdjustmentX = 10;
    private int sizeAdjustmentY = 16;

    void Start()
    {
        ladder = GetComponent<BoxCollider2D>();
        ladder.size = new Vector2(ladder.size.x - sizeAdjustmentX, ladder.size.y - sizeAdjustmentY);
        top = transform.GetChild(0).GetComponent<BoxCollider2D>();
        top.enabled = false;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            top.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            top.enabled = false;
        }
    }
}