
using System.Data;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float jumpForce = 25f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;

    float turnSmoothVelocity;
    Vector3 velocity;
    private bool isGrounded;
    private bool canMove = true;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (Input.GetMouseButton(1))
            {
                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }
        else if (axisName == "Mouse Y")
        {
            if (Input.GetMouseButton(1))
            {
                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axisName);
    }

    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //velocity = moveDir.normalized * speed;
        }
        //else
        //{
        //    velocity = Vector3.zero;
       // }

       // if (isGrounded && Input.GetKeyDown(KeyCode.Space))
       // {
       //     Jump();
        //}
    }

    public void MovePlayer(Vector3 direction, float distance)
    {
        // Ignore any Y-axis changes
        direction.y = 0f;
    
        Vector3 newPosition = transform.position + (direction.normalized * distance);
        controller.Move(newPosition - transform.position);
    }
    
    public void SetPlayerPosition(Vector3 position, Quaternion rotation)
    {
        // Set the player's position to the given position
        controller.enabled = false; // Disable the controller temporarily to set the position
        transform.position = position;
        transform.rotation = rotation;
        controller.enabled = true; // Re-enable the controller
    }
    
    //void FixedUpdate()
   // {
    //    if (canMove)
   //     {
            // Apply gravity to the character controller
   //         velocity.y += gravity * Time.deltaTime;

            // Move the character controller using SimpleMove()
   //         controller.Move(velocity * Time.deltaTime);
     //   }
   // }

   // void Jump()
   // {
   //     velocity.y = jumpForce;
   // }
}
