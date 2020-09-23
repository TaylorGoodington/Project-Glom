using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class LevelZero : MonoBehaviour
{
    private Vector2 normalPlayerStartPosition = new Vector2(10, 29);

    void Start()
    {
        SceneTransitions.Instance.TransitionIn();

        if (GameData.Instance.levelZeroGameState == GameState.Normal)
        {
            #region Cinematics
            //None
            #endregion

            #region Character Positioning
            GameControl.Instance.player.transform.position = normalPlayerStartPosition;
            #endregion

            #region Camera Positioning

            #endregion

            #region Music

            #endregion

            #region UI

            #endregion

            #region Scene Setting

            #endregion

        }
        else if (GameData.Instance.levelZeroGameState == GameState.First_Execution)
        {
            #region Cinematics
            //None
            #endregion

            #region Character Positioning
            GameControl.Instance.player.transform.position = normalPlayerStartPosition;
            #endregion

            #region Camera Positioning

            #endregion

            #region Music

            #endregion

            #region UI

            #endregion

            #region Scene Setting

            #endregion
        }
        else if (GameData.Instance.levelZeroGameState == GameState.Helmet_Info)
        {

        }
        else if (GameData.Instance.levelZeroGameState == GameState.Knight_Info)
        {

        }

        UserInterface.Instance.UpdateUserInterface();

        GameControl.Instance.inputState = InputState.Player_Character;
    }
}