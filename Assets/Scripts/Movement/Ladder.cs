using UnityEngine;

public class Ladder : MonoBehaviour
{
    private BoxCollider2D top;

    void Start()
    {
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