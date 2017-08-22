using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class ProjectileMovement : MonoBehaviour
{
    [HideInInspector] public int spellId;
    private Controller2D controller;
    private int direction;
    private Vector3 velocity;

	void Start ()
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

	void Update ()
    {
        controller.Move(velocity);
	}
}