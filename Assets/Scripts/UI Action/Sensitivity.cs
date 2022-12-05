using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sensitivity : MonoBehaviour
{
    public GameManager gm;


    public void OnFunction()
    {
        gm.currentMouseSensitivity = GetComponent<Slider>().value;
    }
}
