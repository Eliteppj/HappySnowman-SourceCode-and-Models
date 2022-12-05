using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_backTitle : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {
        gm.SavePlayerData();
        gm.SaveGameData();
        gm.BackToTitle();
    }
}
