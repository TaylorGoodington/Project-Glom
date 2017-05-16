using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions instance;

    public Animator animator;
    private string levelDestination;

    void Start ()
    {
        instance = GetComponent<SceneTransitions>();
	}

    public float CurrentAnimationLength ()
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] animatorClip = animator.GetCurrentAnimatorClipInfo(0);
        return animatorClip[0].clip.length * animationState.normalizedTime;
    }

    public void TransitionOut(string destination)
    {
        levelDestination = destination;
        animator.Play("transitionOut");
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(levelDestination);
    }

    public void TransitionIn ()
    {
        animator.Play("transitionIn");
    }
}