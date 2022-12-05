using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDevice : MonoBehaviour
{
    private Vector3 pos;
    public float max;
    public float speed;
    // Update is called once per frame

    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        Vector3 v = pos;
        v.x += max * Mathf.Sin(Time.time*speed);
        this.transform.position = v;
    }
}
