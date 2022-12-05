using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_eixtGame : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {
        gm.SaveGameData();
        gm.SavePlayerData();
        Application.Quit();
    }
}
