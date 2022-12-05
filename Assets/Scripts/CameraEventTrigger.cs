using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventTrigger : MonoBehaviour
{
    public MyCharacter character;
    public Camera cam;
    public bool active;


    public Vector3 char_rot;
    public Vector3 cam_rot;
  
  
    private void OnTriggerEnter(Collider other)
    {
        if(active)
        {
            DoSomething();
            active = false;
        }

        else
        {
            Debug.Log(character.name + "���������� ����");
        }
   
    }
   

    public void DoSomething()
    {
        Debug.Log(character.name + "����");

        cam.GetComponent<FollowCamera>().Offset = cam_rot;
        character.transform.rotation = Quaternion.Euler(char_rot);


    }
}
