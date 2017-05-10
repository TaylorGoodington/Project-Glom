using UnityEngine;

public class StartController : MonoBehaviour
{
    void Start ()
    {
        SceneTransitions.instance.TransitionIn();
    }

    public void GoToLevel (string level)
    {
        SceneTransitions.instance.TransitionOut(level);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}