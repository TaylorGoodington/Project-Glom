using System.Collections;
using UnityEngine;
using static Utility;

public class LevelZero : MonoBehaviour
{
    private Vector2 normalPlayerStartPosition = new Vector2(10, 29);

    void Start()
    {
        SceneTransitions.Instance.TransitionIn();

        StartCoroutine("InitializeLevel", CheckAndRunCinematics());
    }

    //Get cinematic depending on level game state, run it, and return the clip length for the delay in level intialization.
    private float CheckAndRunCinematics()
    {
        float cinematicLength = 0;

        if (GameData.Instance.levelZeroGameState == GameStates.Normal)
        {
            cinematicLength = 0;
        }
        else if (GameData.Instance.levelZeroGameState == GameStates.First_Execution)
        {
            cinematicLength = 5;
        }
        else if (GameData.Instance.levelZeroGameState == GameStates.Helmet_Info)
        {
            cinematicLength = 0;
        }
        else if (GameData.Instance.levelZeroGameState == GameStates.Knight_Info)
        {
            cinematicLength = 0;
        }

        return cinematicLength;
    }

    private IEnumerator InitializeLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (GameData.Instance.levelZeroGameState == GameStates.Normal)
        {
            #region Character Positioning
            GameControl.Instance.player.transform.position = normalPlayerStartPosition;
            #endregion

            #region Scenery

            #endregion

            #region Camera Positioning

            #endregion

            #region Music
            MusicController.Instance.PlayTrack(MusicTracks.Level_Zero_Normal);
            #endregion

            #region UI
            UserInterface.Instance.UpdateUserInterface();
            #endregion

        }
        else if (GameData.Instance.levelZeroGameState == GameStates.First_Execution)
        {
            #region Character Positioning
            GameControl.Instance.player.transform.position = normalPlayerStartPosition;
            #endregion

            #region Scenery

            #endregion

            #region Camera Positioning

            #endregion

            #region Music
            MusicController.Instance.PlayTrack(MusicTracks.Level_Zero_Normal);
            #endregion

            #region UI
            UserInterface.Instance.UpdateUserInterface();
            #endregion
        }
        else if (GameData.Instance.levelZeroGameState == GameStates.Helmet_Info)
        {

        }
        else if (GameData.Instance.levelZeroGameState == GameStates.Knight_Info)
        {

        }

        GameControl.Instance.inputState = InputStates.Player_Character;
    }
}