using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameManager gm;
    public GameObject resetMenu;
    public void OnClick()
    {
        gm.OpenReset();
        gm.CloseOption();
        gm.ClosedGeneralPanel();
        gm.playMode = UniData.PlayMode.TitleUI;
    }
}
