using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VSync : MonoBehaviour
{
    public GameManager gm;

    public void OnFunction()
    {
        gm.VSync(GetComponent<Toggle>().isOn);

        if(GetComponent<Toggle>().isOn==true)
        {
            gm.currentVSync = true;
            gm.currentVSyncInt = 1;
        }
        else
        {
            gm.currentVSync = false;
            gm.currentVSyncInt = 0;
        }
       
    }
}
