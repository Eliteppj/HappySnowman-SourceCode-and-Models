using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggle_fullscreen : MonoBehaviour
{
    public GameManager gm;

    public void OnFunction()
    {
        gm.fullscreen = GetComponent<Toggle>().isOn;
       
        if(gm.fullscreen)
        {
            gm.fullscreenInt = 1;
        }
        else
        {
            gm.fullscreenInt = 0;
        }
     
        gm.ChangeResolution(gm.currentResolutionNumber);
    }
}
