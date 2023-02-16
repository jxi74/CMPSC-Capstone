using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float jumpForce = 25f;
    public float gravity = -19.81f;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canMove = true;

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
        isGrounded = controller.isGrounded;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            velocity = moveDir.normalized * speed;
        }
        else
        {
            velocity = Vector3.zero;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void MovePlayer(Vector3 direction, float distance)
    {
        // Ignore any Y-axis changes
        direction.y = 0f;
    
        Vector3 newPosition = transform.position + (direction.normalized * distance);
        controller.Move(newPosition - transform.position);
    }
    
    public void SetPlayerPosition(Vector3 position)
    {
        // Set the player's position to the given position
        controller.enabled = false; // Disable the controller temporarily to set the position
        transform.position = position;
        controller.enabled = true; // Re-enable the controller
    }
    
    void FixedUpdate()
    {
        if (canMove)
        {
            // Apply gravity to the character controller
            velocity.y += gravity * Time.deltaTime;

            // Move the character controller using SimpleMove()
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Jump()
    {
        velocity.y = jumpForce;
    }
}
