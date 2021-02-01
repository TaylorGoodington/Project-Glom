using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class LevelOne : MonoBehaviour
{
    [SerializeField] LevelGenerator LevelGenerator = null;
    [SerializeField] Transform LevelComponentTransform = null;
    [SerializeField] Player player = null;

    private Vector2 normalPlayerStartPosition = new Vector2(10, 29); private GameState gameState;

    private enum GameState
    {
        First_Execution,
        Normal,
        Knight_Info,
        Helmet_Info,
        Level_One_Completion
    }

    void Start()
    {
        DetermineGameState();
        LevelGenerator.CreateBottomBlock(LevelComponentTransform);
        InitializeLevel();
        CameraController.Instance.SetCameraTarget();
        SceneTransitions.Instance.TransitionIn();
        LevelGenerator.GenerateLevel(LevelComponentTransform);
        CheckAndRunCinematics();
        StartUIAndControl();
    }

    private void DetermineGameState()
    {
        //First Execution
        gameState = GameState.First_Execution;
        //First Death
        //New Enemy Knight
        //New Enemy Helmet
        //Level One Completion
    }

    //Get cinematic depending on level game state, run it, and return the clip length for the delay in level intialization.
    private float CheckAndRunCinematics()
    {
        float cinematicLength = 0;

        if (gameState == GameState.Normal)
        {
            cinematicLength = 0;
        }
        else if (gameState == GameState.First_Execution)
        {
            cinematicLength = 1;
        }
        else if (gameState == GameState.Helmet_Info)
        {
            cinematicLength = 0;
        }
        else if (gameState == GameState.Knight_Info)
        {
            cinematicLength = 0;
        }

        return cinematicLength;
    }

    private void InitializeLevel()
    {
        GameControl.Instance.FindPlayer(player);

        if (gameState == GameState.Normal)
        {
            #region Character Positioning
            GameControl.Instance.player.transform.position = normalPlayerStartPosition;
            #endregion

            #region Scenery

            #endregion

            #region Camera Positioning
            CameraController.Instance.SetCameraTarget();
            #endregion

            #region Music
            MusicController.Instance.PlayTrack(Utility.MusicTrack.Level_One_Normal);
            #endregion

            #region UI
            UserInterface.Instance.UpdateUserInterface();
            #endregion

        }
        else if (gameState == GameState.First_Execution)
        {
            #region Character Positioning
            GameControl.Instance.player.transform.position = normalPlayerStartPosition;
            #endregion

            #region Scenery

            #endregion

            #region Camera Positioning
            CameraController.Instance.SetCameraTarget();
            #endregion

            #region Music
            MusicController.Instance.PlayTrack(Utility.MusicTrack.Level_One_Normal);
            #endregion

            #region UI
            UserInterface.Instance.UpdateUserInterface();
            #endregion
        }
        else if (gameState == GameState.Helmet_Info)
        {

        }
        else if (gameState == GameState.Knight_Info)
        {

        }
    }

    private void StartUIAndControl()
    {
        UserInterface.Instance.UpdateUserInterface();
        GameControl.Instance.inputState = InputState.Player_Character;
    }
}
