using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //CharacterController cc;
    Rigidbody rb;
    public Animator animator;
    public GameManager gm;
    public RoadGenerator rg;
    Vector3 moveVec, gravity;

    float speed = 5;
    float jumpSpeed = 12;
    float realGravity = -9.8f;
    public float jumpPower = 15;
    public float jumpGravity = -40;

    bool isJumping = false;
    public bool canPlay;

    int laneNumber = 1;
    int laneCount = 2;

    public float FirstLanePos;
    public float LaneDistance;
    public float SideSpeed;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        moveVec = new Vector3(1, 0, 0);
        gravity = Vector3.zero;
        SwipeController.SwipeEvent += CheckInput;
        animator.SetTrigger("Run");
    }

    void Update()
    {
       // rb = GetComponent<Rigidbody>();
       // animator = GetComponent<Animator>();

        if(canPlay)
            moveVec.x = speed;

        moveVec += gravity;
        moveVec *= Time.deltaTime;

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;

    }


    void CheckInput(SwipeController.SwipeType type)
    {
        int sign = 0;

        if (!canPlay)
            return;

        if (type == SwipeController.SwipeType.LEFT)
        {
            sign = 1;
        }
        else if (type == SwipeController.SwipeType.RIGHT)
        {
            sign = -1;
        }  
        else if(type == SwipeController.SwipeType.UP) {
            animator.SetTrigger("Jump");
           // animator.SetBool("jump_b", true);
            Jump();
        }
        else
            

        return;

        laneNumber += sign;
        laneNumber = Mathf.Clamp(laneNumber, 0, laneCount);
    }

    void Jump()
    {
        // animator.SetBool("jump_b", true);
        isJumping = true;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());

    }
    IEnumerator StopJumpCoroutine()
    {
        // animator.SetBool("jump_b", false);
        animator.SetTrigger("Run");
        do
        {
            yield return new WaitForSeconds(0.02f);
        } while (rb.velocity.y != 0);
        isJumping = false;
        Physics.gravity = new Vector3(0, realGravity, 0);
        
    }

    public void StartGame()
    {
        
        RoadGenerator.instance.StartLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lose")) {
            rg.speed = 0;
            animator.SetTrigger("Death");
            StartCoroutine(Death());
        }
        else if (other.CompareTag("Coin"))
        {
            gm.AddCoins(1);
        }
            
        // Destroy(other.gameObject);
    }

    IEnumerator Death()
    {
        //animator.SetTrigger("Run");
        canPlay = false;
        yield return new WaitForSeconds(1);
        gm.ShowResult();
    }


}
