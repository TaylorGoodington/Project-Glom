using UnityEngine;
using System.Collections;

public class DissolvingPlatformController : MonoBehaviour {

    private Collider2D boxCollider;
    private Animator animator;

	void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
	}

    public void StartDissolving()
    {
        animator.Play("DissolvingPlatform");
    }

    public void ToggleBoxCollider()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }

    public void Waiting(){}

    public void OnTriggerEnter2D(Collider2D collider)
    {
        float colliderTopPoint = GetComponent<Collider2D>().bounds.max.y;
        float playerBottomPoint = collider.GetComponent<Collider2D>().bounds.min.y;
        float differenceInColliders = Mathf.Abs(playerBottomPoint - colliderTopPoint);
        if (collider.gameObject.tag == "Player")
        {
            if (differenceInColliders < .5)
            {
                Invoke("Waiting", 0.3f);
                if (differenceInColliders < .5)
                {
                    StartDissolving();
                }
            }
        }
    }
}
