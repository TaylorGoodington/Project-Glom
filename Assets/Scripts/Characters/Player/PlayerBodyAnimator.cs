using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyAnimator : MonoBehaviour
{
    //Animation Event
    private void DyingComplete()
    {
        GameControl.Instance.PlayerDeathAnimationIsComplete();
    }
}