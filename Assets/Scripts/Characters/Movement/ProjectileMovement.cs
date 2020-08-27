using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class ProjectileMovement : MonoBehaviour
{
    [HideInInspector]
    public int spellId;
    private Controller2D controller;
    private int direction;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        GetComponent<Animator>().Play(SpellDatabase.spells[spellId].name);

        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        velocity.y = 0;
        velocity.x = SpellDatabase.spells[spellId].projectileSpeed * direction;
    }

    void Update()
    {
        if (velocity.x == 0)
        {
            StartCoroutine(Deterierate());
        }
        else
        {
            controller.Move(velocity, Vector2.zero);

            if (transform.position.x < -10 || transform.position.x > 180)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
        {
            if (collision is BoxCollider2D)
            {
                velocity.x = 0;
                collision.GetComponent<EnemyStats>().currentHp -= (int)SpellDatabase.spells[spellId].baseDamage;
            }
        }
    }

    IEnumerator Deterierate()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}