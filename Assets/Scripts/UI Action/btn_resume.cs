using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_resume : MonoBehaviour
{
    public GameManager gm;
   public void OnClick()
    {
        gm.ClosePause();
        gm.playerController.canControl = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gm.playMode = UniData.PlayMode.Gameplay;
    }
}
