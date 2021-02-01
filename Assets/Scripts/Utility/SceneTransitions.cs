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

    public float TransitionOut()
    {
        GameControl.Instance.inputState = InputState.None;
        animator.Play("transitionOut");
        CameraController.Instance.ClearCameraTarget();
        return CurrentAnimationLength();
    }


    public float TransitionIn ()
    {
        GameControl.Instance.inputState = InputState.None;
        animator.Play("transitionIn");
        return CurrentAnimationLength();
    }

    private float CurrentAnimationLength()
    {
        AnimatorClipInfo[] animatorClip = animator.GetCurrentAnimatorClipInfo(0);
        return animatorClip[0].clip.length;
    }
}