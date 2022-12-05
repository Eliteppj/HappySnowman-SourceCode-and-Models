using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_Reset_Yes : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {
        gm.ResetGame();
        gm.CloseReset();


        gm.BackToTitle();




    }
}
