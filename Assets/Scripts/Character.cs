using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class Character : MonoBehaviour
{

    public Rigidbody rigid;
    public Vector3 moveDirection;
    public float moveSpeed;
    public float maxSpeed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rigid.velocity.magnitude>maxSpeed)
        {
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed);
        }

       
    }
       

    public void Move(float axisV, float axisH, bool jump)
    {
       if((axisV==0.0f)&&(axisH==0.0f))
        {
            rigid.velocity = Vector3.zero;
        }

        moveDirection = new Vector3(-axisH,0, -axisV).normalized;
        moveDirection = transform.TransformDirection(moveDirection);
        rigid.AddForce(moveDirection*moveSpeed);


        
    }

 



 

 
}
