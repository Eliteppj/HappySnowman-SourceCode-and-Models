using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager; //게임매니저
    public ControlMode controlMode = ControlMode._2D; //현재 조종모드
    public TEST_CharacterMovement Player; //조종할 캐릭터
    public bool canControl;
    //public Character Player2;

    private bool bForward;
    private bool bBackwards;
    private bool bLeft;
    private bool bRight;
    private bool bJump;
    private bool bFire;

    private int axisV_Raw;
    private int axisH_Raw;

    public float axisV;
    public float axisH;

    

    private float mouse_x;
    private float mouse_y;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;

    }


    private void Update()
    {


        if (canControl) //조작 가능한 모드일 때만 입력을 받음
        {
            switch (controlMode)
            {
                case ControlMode._2D:
                    ControlGroup_2D();
                    break;

                case ControlMode._3D:
                    ControlGroup_3D();
                    break;

                case ControlMode._2DAxisRaw:
                    ControlGroup_2DAxisRaw();
                    break;

                case ControlMode._3DAxisRaw:
                    ControlGroup_3DAxisRaw();
                    break;

                case ControlMode._3DAxis:
                    ControlGroup_3DAxis();
                    break;

                case ControlMode._2DAxis:
                    ControlGroup_2DAxis();
                    break;

            }

            if (Input.GetKey(gameManager.playerdata.kFire)) //공격
            {
                bFire = true;
            }
            else
            {
                bFire = false;
            }

            if (Input.GetKey(KeyCode.E)) //E키
            {
                //레이를 쏴서 상점이라면 오픈함
            }
        }
        else //입력불가능 상태일 때 Move함수를 직접 호출하여 0으로 넣어줘야 점프 하고 퍼즈했을 때 정상적으로 밑으로 떨어지며, else문 이하가 없을 경우 Move입력 자체가 없어서 제자리에서 멈춰버리며 애니메이션도 그대로 실행이되어 이상해짐
        {
            Player.Move(0, 0, false, 0, 0,0);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (gameManager.playMode != UniData.PlayMode.TitleUI)) //ESC키는 조종불가 상태에서도 입력을 받음, 단 타이틀 화면이 아닐때만
        {

            if (gameManager.playMode == UniData.PlayMode.Gameplay) //게임플레이 중이었다면
            {
                canControl = false; //컨트롤 불가
                gameManager.OpenPause();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                gameManager.playMode = UniData.PlayMode.PauseUI;
            }
            else if (gameManager.playMode == UniData.PlayMode.PauseUI) //퍼즈상태였다면
            {
                canControl = true; //컨트롤 가능
                gameManager.ClosePause();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                gameManager.playMode = UniData.PlayMode.Gameplay;
            }
            else if (gameManager.playMode == UniData.PlayMode.SubUI) //서브 UI였다면
            {
                canControl = false;
                gameManager.OpenPause();
                gameManager.CloseTitle();
                gameManager.CloseCredit();
                gameManager.CloseOption();
                gameManager.CloseHelp();
                gameManager.ClosedGeneralPanel();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                gameManager.playMode = UniData.PlayMode.PauseUI;


            }



        }




    }




    private void ControlGroup_2D()
    {
        if (Input.GetKey(gameManager.playerdata.kLeft)) //좌
        {
            bLeft = true;
        }
        else
        {
            bLeft = false;
        }
        if (Input.GetKey(gameManager.playerdata.kRight)) //우
        {
            bRight = true;
        }
        else
        {
            bRight = false;
        }
        if (Input.GetKey(gameManager.playerdata.kJump)) //점프
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //공격
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        //Player.Move(bForward, bBackwards, bLeft, bRight, bJump);
    } //좌우 이동, bool 리턴

    private void ControlGroup_3D()
    {
        if (Input.GetKey(gameManager.playerdata.kForward)) //전
        {
            bForward = true;
        }
        else
        {
            bForward = false;
        }
        if (Input.GetKey(gameManager.playerdata.kBackward)) //후
        {
            bBackwards = true;
        }
        else
        {
            bBackwards = false;
        }
        if (Input.GetKey(gameManager.playerdata.kLeft)) //좌
        {
            bLeft = true;
        }
        else
        {
            bLeft = false;
        }
        if (Input.GetKey(gameManager.playerdata.kRight)) //우
        {
            bRight = true;
        }
        else
        {
            bRight = false;
        }
        if (Input.GetKey(gameManager.playerdata.kJump)) //점프
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //공격
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        //Player.Move(bForward, bBackwards, bLeft, bRight, bJump);
    } //전후좌우 이동, bool 리턴

    private void ControlGroup_2DAxisRaw()
    {
        if (Input.GetKey(gameManager.playerdata.kLeft)) //좌
        {
            axisH = -1;
        }
        else if (Input.GetKey(gameManager.playerdata.kRight))
        {
            axisH = 1;
        }
        else
        {
            axisH = 0;
        }
        if (Input.GetKeyDown(gameManager.playerdata.kJump)) //점프
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //공격
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        //Player.MoveOrder(axisV, axisH, bJump);
    } //좌우 이동, 축(int) 리턴

    private void ControlGroup_3DAxisRaw()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        if (Input.GetKey(gameManager.playerdata.kForward))
        {
            axisV = 1;
        }
        else if (Input.GetKey(gameManager.playerdata.kBackward))
        {
            axisV = -1;
        }
        else
        {
            axisV = 0;
        }
        if (Input.GetKey(gameManager.playerdata.kLeft)) //좌
        {
            axisH = -1;
        }
        else if (Input.GetKey(gameManager.playerdata.kRight))
        {
            axisH = 1;
        }
        else
        {
            axisH = 0;
        }
        if (Input.GetKey(gameManager.playerdata.kJump)) //점프
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //공격
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        Player.Move(axisV, axisH, bJump, mouse_x, mouse_y, gameManager.currentMouseSensitivity);
    } //전후좌우 이동, 축(int) 리턴

    private void ControlGroup_2DAxis() //단 a,d이동이며 w,s는 따로 안 만듬 어차피 w와 s만 쓸 일이 없음
    {
        if (Input.GetKey(gameManager.playerdata.kLeft))
        {

            Debug.Log(Input.GetAxis("Horizontal"));
        }
        else if (Input.GetKey(gameManager.playerdata.kRight))
        {
            Debug.Log(Input.GetAxis("Horizontal"));
        }


    }

    private void ControlGroup_3DAxis()
    {

        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        if (Input.GetKey(gameManager.playerdata.kLeft)) //왼쪽이동 키 눌렸을 때
        {

            // Debug.Log(Input.GetAxis("Horizontal"));
            //axisH = Input.GetAxis("Horizontal"); //커스텀 키가 아님

            if (axisH > 0) //왼쪽으로 가는 키 입력했는데 현재 오른쪽 이동값이 남아있을 때 즉시 0으로 초기화하고 진행함 (관성 없애기)
            {
                axisH = 0;
            }
            axisH -= Time.deltaTime * 3f; //천천히 증가시킴


        }
        else if (Input.GetKey(gameManager.playerdata.kRight))
        {
            //Debug.Log(Input.GetAxis("Horizontal"));
            //axisH = Input.GetAxis("Horizontal");
            if (axisH < 0)
            {
                axisH = 0;
            }
            axisH += Time.deltaTime * 3f;

        }

        else
        {
            // Debug.Log(Input.GetAxis("Horizontal"));
            // axisH = Input.GetAxis("Horizontal");
            axisH = 0;

        }
        axisH = Mathf.Clamp(axisH, -1f, 1f);

        if (Input.GetKey(gameManager.playerdata.kForward))
        {

            //  Debug.Log(Input.GetAxis("Vertical"));
            //axisV = Input.GetAxis("Vertical");

            if (axisV < 0)
            {
                axisV = 0;
            }
            axisV += Time.deltaTime * 3f;

        }


        else if (Input.GetKey(gameManager.playerdata.kBackward))
        {
            //  Debug.Log(Input.GetAxis("Vertical"));
            // axisV = Input.GetAxis("Vertical");
            if (axisV > 0)
            {
                axisV = 0;
            }
            axisV -= Time.deltaTime * 3f;

        }
        else
        {
            // Debug.Log(Input.GetAxis("Vertical"));
            // axisV = Input.GetAxis("Vertical");
            axisV = 0;

        }
        axisV = Mathf.Clamp(axisV, -1f, 1f);
        if (Input.GetKey(gameManager.playerdata.kJump)) //점프
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }

        if(Input.GetKeyDown(gameManager.playerdata.kInteraction))
        {
            Player.CheckPointRaycast();
        }

        Player.Move(axisV, axisH, bJump, mouse_x, mouse_y, gameManager.currentMouseSensitivity);
        
    }

}
