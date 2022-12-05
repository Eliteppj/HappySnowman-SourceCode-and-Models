using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_play : MonoBehaviour
{
    public GameManager gm;

    public void OnClick()
    {
        gm.PlayGame();
    }
}
