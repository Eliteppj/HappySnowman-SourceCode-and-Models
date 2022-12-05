using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Vector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.GetComponent<Rigidbody>())
        {
           // this.transform.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * 50 * 3);
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        Direction = new Vector3(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
       

        this.GetComponent<Rigidbody>().AddForce(Direction*3);

    }
}
