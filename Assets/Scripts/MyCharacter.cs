using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class MyCharacter : MonoBehaviour
{

    public CharacterLevel level;

    public CharacterController controller;
    public Rigidbody rb;
    public SphereCollider sphereCollider;

    public Mesh character_mesh_snowman;
    public Mesh character_mesh_ball;

    
    public Vector3 playerVelocity_forGravity;
    public bool groundedPlayer;
    public float GroundCheckOffset;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = 9.81f;
    public float airspeed = 1.0f;

    public Vector3 lastDirection;

    public Vector3 direction;

    public RaycastHit slopehit;

    public Vector3 tmp1;


    public GameManager tmp11;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ChangeLevel(tmp11.playerdata.characterLevel);
        
       
        
    }

    public void MoveOrder(float axisV, float axisH, bool jump)
    {

        switch (level)
        {

            case CharacterLevel.Lv1:
                MoveAsBall(axisV, axisH, jump);
                break;

            case CharacterLevel.Lv2:
                Move(axisV, axisH, jump);
                break;

            case CharacterLevel.Lv3:
                Move(axisV, axisH, jump);
                Attack();
                break;
        }
    } //�ܺο��� ȣ��ǰ�, ���⿡�� Move�Լ��� ���� ȣ����

    private void Move(int axisV, int axisH, bool jump)
    {



        //Debug.Log(axisV+""+axisH+""+ jump);
        direction = new Vector3(-axisH, 0, -axisV).normalized;


        direction = transform.TransformDirection(direction);




        if (isGrounded() && OnSlope())
        {
            direction = GetSl();
            playerVelocity_forGravity.y = 0;
            controller.Move(direction * Time.deltaTime * playerSpeed);
            lastDirection = direction;

        }
        else if (isGrounded())
        {
            playerVelocity_forGravity.y = 0;
            controller.Move(direction * Time.deltaTime * playerSpeed);
            lastDirection = direction;
        }
        else //���߿� ���� ��
        {
            //direction = lastDirection + ((lastDirection - direction) * airspeed);
            controller.Move(direction * Time.deltaTime * playerSpeed);

        }


        if (jump && isGrounded() && (playerVelocity_forGravity.y <= 0))
        {
            playerVelocity_forGravity.y += jumpHeight;
            lastDirection = direction;
        }

        playerVelocity_forGravity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity_forGravity * Time.deltaTime);





    } //Not used in HappySnowman

    public void Move(bool forward, bool backward, bool left, bool right, bool jump)
    {
        Debug.Log(forward + "" + backward + "" + left + "" + right + "" + jump);

    }

    private void Move(float axisV, float axisH, bool jump)
    {


        //Debug.Log(axisV+""+axisH+""+ jump);
        direction = new Vector3(-axisH, 0, -axisV).normalized;
        direction = transform.TransformDirection(direction);




        if (isGrounded_sc() && OnSlope())
        {
            direction = GetSl();
            playerVelocity_forGravity.y = 0;
            controller.Move(direction * Time.deltaTime * playerSpeed);
            lastDirection = direction;

        }
        else if (isGrounded_sc())
        {
            playerVelocity_forGravity.y = 0;
            controller.Move(direction * Time.deltaTime * playerSpeed);
            lastDirection = direction;

        }
        else //���߿� ���� ��
        {
            controller.Move(direction * Time.deltaTime * airspeed);
            lastDirection = direction;
        }


        if (jump && isGrounded_sc() && (playerVelocity_forGravity.y <= 0))
        {
            playerVelocity_forGravity.y += jumpHeight;
            lastDirection = direction;
        }

        playerVelocity_forGravity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity_forGravity * Time.deltaTime);




        if (Heading())
        {
            playerVelocity_forGravity.y -= gravityValue * Time.deltaTime * 2;
            controller.Move(playerVelocity_forGravity * Time.deltaTime);
        }

    }//Lv 2�̻��� �� ������ �ڵ�, ���� ��� �ڵ�

    private void MoveAsBall(float axisV, float axisH, bool jump) //���� �� ������ �ڵ�
    {
        Vector3 Direction = new Vector3(-axisH, 0, -axisV);
        rb.AddForce(Direction * 1f);
      
    }

    public void Attack() //���� �ڵ�
    {

    }

    public bool isGrounded()
    {
        if (Physics.Raycast(transform.TransformPoint(controller.center), -Vector3.up, controller.height - controller.center.y + GroundCheckOffset))
        {
            //Debug.Log(true);
            //Debug.DrawLine(transform.TransformPoint(controller.center), transform.TransformPoint(controller.center) + new Vector3(0, -1.1f, 0), Color.blue, 10f);
            return true;
        }
        else
        {
            //Debug.Log(false);
            return false;
        }

    }


    public bool isGrounded_sc()
    {
        var castOrigin = controller.center - new Vector3(0, 0.1f, 0);
        castOrigin = transform.TransformPoint(castOrigin);

        Physics.SphereCast(castOrigin, controller.radius + 0.1f, Vector3.down, out var hit, GroundCheckOffset, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

        if (hit.collider != null)
        {
            // Debug.Log("Ground!"+hit.collider.name);
            return true;

        }
        else
        {
            // Debug.Log("Not Grounded!");
            return false;
        }

        //Debug.DrawRay(this.transform.position,hit.normal, Color.red,10f);
        //Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(direction,hit.normal),Color.green,10f);




    }

    public bool Heading()
    {

        var castOrigin = controller.center + new Vector3(0, controller.height / 2, 0);
        castOrigin = transform.TransformPoint(castOrigin);
        Debug.DrawRay(castOrigin, Vector3.up * 0.1f, Color.blue);
        if (Physics.Raycast(castOrigin, Vector3.up, 0.1f))
        {
            //Debug.Log("head");
            return true;
        }
        return false;

    }

    public void SlopeSliding()
    {
        var castOrigin = controller.center - new Vector3(0, 0.1f, 0);

        if (Physics.SphereCast(castOrigin, controller.radius + 0.1f, Vector3.down, out var hit, GroundCheckOffset, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
        {
            var angle = Vector3.Angle(Vector3.up, hit.normal);
            var tmp1 = Vector3.Cross(hit.normal, Vector3.up);
            var tmp2 = Vector3.Cross(hit.normal, tmp1);




            // Debug.Log(hit.normal);
            // Debug.Log(angle);
            //Debug.Log(tmp1);
            //Debug.Log(tmp2);




            //Debug.DrawRay(this.transform.position,hit.normal, Color.red,10f);
            //Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(direction,hit.normal),Color.green,10f);



        }
    }

    public bool OnSlope()
    {
        if ((Physics.Raycast(transform.TransformPoint(controller.center), Vector3.down, out slopehit, controller.height - controller.center.y + GroundCheckOffset,~LayerMask.GetMask("Player"))))
        {

            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle < controller.slopeLimit && angle != 0;
        }
        return false;
    }

    public Vector3 GetSl()
    {
        return Vector3.ProjectOnPlane(direction, slopehit.normal).normalized;
    }



    public void Fix1() //���� �浹 �� ������ ����ȭ
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        
    }

    /*****************************************************************************************************************/

    public bool ChangeLevel(CharacterLevel _level)
    {
        if (level == _level) //�̹� ���� �����̶�� ���� else������ �ڵ带 �������� �ʴ´�.
        {
            return false;
        }
        else //���� ���� �ƴ� ��
        {
            if (_level == CharacterLevel.Lv2 || _level == CharacterLevel.Lv3) //�Ű������� 2 �Ǵ� 3�� ��
            {
                if (level != CharacterLevel.Lv1) //���� ������ 2 �Ǵ� 3�� �� ���� ����
                {
                    return false;
                }
                else //���� ������ 1�� �� ���� ��
                {
                    level = _level;
                    ChangeMovementPolicy();
                    return true;
                }
            }
            else //�Ű������� 1�� ��, �׷��� 290Line���� �Ű������� 1�� ���� �� �� �������� ���� ���� 2 �Ǵ� 3�� ���̹Ƿ� �����Ѵ�.
            {
                level = _level;
                ChangeMovementPolicy();
                return true;
            }

        }
    }

    public void ChangeMovementPolicy()
    {
        switch (level)
        {
            case CharacterLevel.Lv1: //�������� ���������ϹǷ� Rigidbody ���ְ� CharacterController ��
                controller.enabled = false;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                rb.useGravity = true;
                sphereCollider.enabled = true;
                controller.detectCollisions = false;
                break;

            case CharacterLevel.Lv2: //CharacterController ����
                controller.enabled = true;
                rb.isKinematic = true;
                rb.detectCollisions = false;
                rb.useGravity = false;
                sphereCollider.enabled = false;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                break;

            case CharacterLevel.Lv3:
                controller.enabled = true;
                rb.isKinematic = true;
                rb.detectCollisions = false;
                rb.useGravity = false;
                sphereCollider.enabled = false;
                break;
        }
    }


}
