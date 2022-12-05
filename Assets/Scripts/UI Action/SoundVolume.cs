using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolume : MonoBehaviour
{
    public GameManager gm;


    public void OnFunction()
    {
        gm.audioSource.volume = GetComponent<Slider>().value;
    }
}
