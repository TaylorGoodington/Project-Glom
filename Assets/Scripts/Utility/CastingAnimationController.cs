using UnityEngine;

public class CastingAnimationController : MonoBehaviour
{
    private int frame;
    [HideInInspector] public int castingPhase;

	void Start ()
    {
        frame = 1;
        castingPhase = 0;
    }

    public void InitiateAnimationSpriteNumber ()
    {
        frame = 1;
        castingPhase = frame;
    }

    public void UpdateAnimationSpriteNumber ()
    {
        frame++;
        if (frame == 1 || frame >= 4)
        {
            castingPhase = 1;
        }
        else
        {
            castingPhase = 2;
        }
    }

    public void CastComplete ()
    {
        gameObject.transform.parent.GetComponent<Player>().casting = false;
        castingPhase = 0;
    }
}