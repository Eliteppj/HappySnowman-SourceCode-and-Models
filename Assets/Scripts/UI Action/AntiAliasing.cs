using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AntiAliasing : MonoBehaviour
{
    public GameManager gm;
    public void OnFunction()
    {
        gm.currentAntiAilasing = GetComponent<TMP_Dropdown>().value;
        gm.AntiAliasing(gm.currentAntiAilasing);
    }
}
