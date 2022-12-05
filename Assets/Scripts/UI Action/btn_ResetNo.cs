using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_ResetNo : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {

        gm.CloseReset();

        gm.BackToTitle();


    }
}
