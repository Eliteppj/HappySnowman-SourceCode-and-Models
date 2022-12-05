using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class DevDebug : MonoBehaviour
{
    public MyRandom random;
    public double[] a;
    public int[] b;
    private void Start()
    {
        random = new MyRandom();
        random.SetWeightbyItemCount(b);
        

    }

 

}