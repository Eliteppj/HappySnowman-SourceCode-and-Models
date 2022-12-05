using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager; //���ӸŴ���
    public ControlMode controlMode = ControlMode._2D; //���� �������
    public TEST_CharacterMovement Player; //������ ĳ����
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


        if (canControl) //���� ������ ����� ���� �Է��� ����
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

            if (Input.GetKey(gameManager.playerdata.kFire)) //����
            {
                bFire = true;
            }
            else
            {
                bFire = false;
            }

            if (Input.GetKey(KeyCode.E)) //EŰ
            {
                //���̸� ���� �����̶�� ������
            }
        }
        else //�ԷºҰ��� ������ �� Move�Լ��� ���� ȣ���Ͽ� 0���� �־���� ���� �ϰ� �������� �� ���������� ������ ��������, else�� ���ϰ� ���� ��� Move�Է� ��ü�� ��� ���ڸ����� ��������� �ִϸ��̼ǵ� �״�� �����̵Ǿ� �̻�����
        {
            Player.Move(0, 0, false, 0, 0,0);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (gameManager.playMode != UniData.PlayMode.TitleUI)) //ESCŰ�� �����Ұ� ���¿����� �Է��� ����, �� Ÿ��Ʋ ȭ���� �ƴҶ���
        {

            if (gameManager.playMode == UniData.PlayMode.Gameplay) //�����÷��� ���̾��ٸ�
            {
                canControl = false; //��Ʈ�� �Ұ�
                gameManager.OpenPause();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                gameManager.playMode = UniData.PlayMode.PauseUI;
            }
            else if (gameManager.playMode == UniData.PlayMode.PauseUI) //������¿��ٸ�
            {
                canControl = true; //��Ʈ�� ����
                gameManager.ClosePause();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                gameManager.playMode = UniData.PlayMode.Gameplay;
            }
            else if (gameManager.playMode == UniData.PlayMode.SubUI) //���� UI���ٸ�
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
        if (Input.GetKey(gameManager.playerdata.kLeft)) //��
        {
            bLeft = true;
        }
        else
        {
            bLeft = false;
        }
        if (Input.GetKey(gameManager.playerdata.kRight)) //��
        {
            bRight = true;
        }
        else
        {
            bRight = false;
        }
        if (Input.GetKey(gameManager.playerdata.kJump)) //����
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //����
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        //Player.Move(bForward, bBackwards, bLeft, bRight, bJump);
    } //�¿� �̵�, bool ����

    private void ControlGroup_3D()
    {
        if (Input.GetKey(gameManager.playerdata.kForward)) //��
        {
            bForward = true;
        }
        else
        {
            bForward = false;
        }
        if (Input.GetKey(gameManager.playerdata.kBackward)) //��
        {
            bBackwards = true;
        }
        else
        {
            bBackwards = false;
        }
        if (Input.GetKey(gameManager.playerdata.kLeft)) //��
        {
            bLeft = true;
        }
        else
        {
            bLeft = false;
        }
        if (Input.GetKey(gameManager.playerdata.kRight)) //��
        {
            bRight = true;
        }
        else
        {
            bRight = false;
        }
        if (Input.GetKey(gameManager.playerdata.kJump)) //����
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //����
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        //Player.Move(bForward, bBackwards, bLeft, bRight, bJump);
    } //�����¿� �̵�, bool ����

    private void ControlGroup_2DAxisRaw()
    {
        if (Input.GetKey(gameManager.playerdata.kLeft)) //��
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
        if (Input.GetKeyDown(gameManager.playerdata.kJump)) //����
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //����
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        //Player.MoveOrder(axisV, axisH, bJump);
    } //�¿� �̵�, ��(int) ����

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
        if (Input.GetKey(gameManager.playerdata.kLeft)) //��
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
        if (Input.GetKey(gameManager.playerdata.kJump)) //����
        {
            bJump = true;
        }
        else
        {
            bJump = false;
        }
        if (Input.GetKey(gameManager.playerdata.kFire)) //����
        {
            bFire = true;
        }
        else
        {
            bFire = false;
        }
        Player.Move(axisV, axisH, bJump, mouse_x, mouse_y, gameManager.currentMouseSensitivity);
    } //�����¿� �̵�, ��(int) ����

    private void ControlGroup_2DAxis() //�� a,d�̵��̸� w,s�� ���� �� ���� ������ w�� s�� �� ���� ����
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

        if (Input.GetKey(gameManager.playerdata.kLeft)) //�����̵� Ű ������ ��
        {

            // Debug.Log(Input.GetAxis("Horizontal"));
            //axisH = Input.GetAxis("Horizontal"); //Ŀ���� Ű�� �ƴ�

            if (axisH > 0) //�������� ���� Ű �Է��ߴµ� ���� ������ �̵����� �������� �� ��� 0���� �ʱ�ȭ�ϰ� ������ (���� ���ֱ�)
            {
                axisH = 0;
            }
            axisH -= Time.deltaTime * 3f; //õõ�� ������Ŵ


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
        if (Input.GetKey(gameManager.playerdata.kJump)) //����
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
