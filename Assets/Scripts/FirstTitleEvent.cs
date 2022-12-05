using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTitleEvent : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Character;
    public Vector3 First_Char_Pos;
    public Vector3 First_Cam_Pos;
    public Camera TitleCam;

    public void StartGame()
    {
        if(gameManager.isGameStart==true)
        {
            Character.transform.position = First_Char_Pos;
            Destroy(TitleCam);
            Destroy(this);
        }
    }

    private void Update()
    {
        if (gameManager.isGameStart == true)
        {
            Character.transform.position = First_Char_Pos;
            Destroy(TitleCam);
            Destroy(this);
        }
    }
}
