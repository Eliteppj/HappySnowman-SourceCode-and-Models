using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_openMenu1 : MonoBehaviour
{
    public GameObject title;
    public GameObject target_1;
    public GameObject target_2;

    public void OnClick()
    {
        title.SetActive(false);
        target_1.SetActive(true);
        target_2.SetActive(true);
    }
}
