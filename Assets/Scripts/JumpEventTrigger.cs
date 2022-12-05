using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEventTrigger : MonoBehaviour
{
    public float power;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("aa");
        }
    }
}
