using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnd : MonoBehaviour
{
    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gm.EndGame();
        }
    }
}
