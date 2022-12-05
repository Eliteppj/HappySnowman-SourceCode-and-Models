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
    } //외부에서 호출되고, 여기에서 Move함수를 직접 호출함

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
        else //공중에 있을 때
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
        else //공중에 있을 때
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

    }//Lv 2이상일 때 움직임 코드, 실제 사용 코드

    private void MoveAsBall(float axisV, float axisH, bool jump) //공일 때 움직임 코드
    {
        Vector3 Direction = new Vector3(-axisH, 0, -axisV);
        rb.AddForce(Direction * 1f);
      
    }

    public void Attack() //공격 코드
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



    public void Fix1() //발판 충돌 시 움직임 동기화
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        
    }

    /*****************************************************************************************************************/

    public bool ChangeLevel(CharacterLevel _level)
    {
        if (level == _level) //이미 같은 레벨이라면 굳이 else이하의 코드를 실행하지 않는다.
        {
            return false;
        }
        else //같은 값이 아닐 때
        {
            if (_level == CharacterLevel.Lv2 || _level == CharacterLevel.Lv3) //매개변수가 2 또는 3일 때
            {
                if (level != CharacterLevel.Lv1) //현재 레벨이 2 또는 3일 때 변경 안함
                {
                    return false;
                }
                else //현재 레벨이 1일 때 변경 함
                {
                    level = _level;
                    ChangeMovementPolicy();
                    return true;
                }
            }
            else //매개변수가 1일 때, 그런데 290Line에서 매개변수는 1은 절대 될 수 없음으로 현재 값은 2 또는 3일 것이므로 변경한다.
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
            case CharacterLevel.Lv1: //데구르르 굴러가야하므로 Rigidbody 켜주고 CharacterController 끔
                controller.enabled = false;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                rb.useGravity = true;
                sphereCollider.enabled = true;
                controller.detectCollisions = false;
                break;

            case CharacterLevel.Lv2: //CharacterController 켜줌
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
