using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_help : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {
        if (gm.playMode == UniData.PlayMode.TitleUI)
        {

        }
        else if (gm.playMode == UniData.PlayMode.PauseUI)
        {
            gm.playMode = UniData.PlayMode.SubUI;
        }
        gm.CloseTitle();
        gm.ClosePause();
        gm.CloseCredit();
        gm.CloseOption();
        gm.OpenHelp();
        gm.OpenGeneralPanel();
    }
}
