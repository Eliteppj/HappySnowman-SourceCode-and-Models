using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResizeWindow : MonoBehaviour
{
    public GameManager gm;
    public void OnFunction()
    {
        gm.currentResolutionNumber = GetComponent<TMP_Dropdown>().value;
        gm.ChangeResolution(gm.currentResolutionNumber);
    }
}
