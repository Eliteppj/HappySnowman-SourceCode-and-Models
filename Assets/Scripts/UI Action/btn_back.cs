using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_back : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {

        if(gm.playMode==UniData.PlayMode.TitleUI)
        {
            gm.OpenTitle();
            gm.CloseCredit();
            gm.CloseOption();
            gm.CloseHelp();
            gm.ClosedGeneralPanel();
        }
        else if(gm.playMode == UniData.PlayMode.SubUI)
        {
            gm.OpenPause();
            gm.CloseCredit();
            gm.CloseOption();
            gm.ClosedGeneralPanel();
            gm.CloseHelp();
            gm.playMode = UniData.PlayMode.PauseUI;
        }
        gm.SavePlayerData();
       


    }
}
