using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;

    Vector3 moveVec, gravity;

    float speed = 5;
    float jumpSpeed = 12;

    int laneNumber = 1;
    int laneCount = 2;

    public float FirstLanePos;
    public float LaneDistance;
    public float SideSpeed;

    bool didChangeLastFrame = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        moveVec = new Vector3(1, 0, 0);
        gravity = Vector3.zero;

       // startPosition = transform.position;
        SwipeController.SwipeEvent += CheckInput;

    }

   
    void Update()
    {
        if (cc.isGrounded)
        {
            gravity = Vector3.zero;

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                gravity.y = jumpSpeed;
            }
        }
        else
        {
            gravity += Physics.gravity * Time.deltaTime * 3;
        }

        moveVec.x = speed;
        moveVec += gravity;
        moveVec *= Time.deltaTime;

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;

        cc.Move(moveVec);
    }


    void CheckInput(SwipeController.SwipeType type)
    {

        int sign = 0;
        /*
            if (isGrounded() && GM.CanPlay && !isRolling)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                    wannaJump = true;
                else if (Input.GetAxisRaw("Vertical") < 0)
                    StartCoroutine(DoRoll());
            }
        */

        if (type == SwipeController.SwipeType.LEFT)
            sign = 1;
        else if (type == SwipeController.SwipeType.RIGHT)
            sign = -1;
        else
            return;


        laneNumber += sign;
        laneNumber = Mathf.Clamp(laneNumber, 0, laneCount);
    }

}
