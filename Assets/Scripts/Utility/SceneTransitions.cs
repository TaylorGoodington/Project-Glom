using UnityEngine;
using static Utility;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Instance;

    public Animator animator;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public float CurrentAnimationLength ()
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] animatorClip = animator.GetCurrentAnimatorClipInfo(0);
        return animatorClip[0].clip.length * animationState.normalizedTime;
    }

    public void TransitionOut()
    {
        animator.Play("transitionOut");
    }


    public void TransitionIn ()
    {
        animator.Play("transitionIn");
    }
}