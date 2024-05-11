using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Move_New : MonoBehaviour
{
    public float speed = 1f;
    private Animator anim;
    private Vector2 movementInput;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool canMove = true; // Flag to control movement
    private bool canWalkLeft = true; // Flag to control walking left
    private bool canWalkRight = true; // Flag to control walking right

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
        Jump();
        Crouch();
        CheckScreenBounds();
    }

    public void OnMovementEvent(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    void Move()
    {
        if (!isJumping) // Only allow movement if not jumping
        {
            float horizontalInput = movementInput.x;
            if (horizontalInput > 0 && canWalkRight) // Moving right
            {
                anim.SetBool("Forward", true);
                anim.SetBool("Backward", false);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (horizontalInput < 0 && canWalkLeft) // Moving left
            {
                anim.SetBool("Forward", false);
                anim.SetBool("Backward", true);
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else // No movement
            {
                anim.SetBool("Forward", false);
                anim.SetBool("Backward", false);
            }
        }
    }

    void Jump()
    {
        if (movementInput.y > 0 && !isJumping && !isCrouching)
        {
            isJumping = true;
            canMove = false; // Disable movement while jumping
            anim.SetTrigger("Jump");
            StartCoroutine(ResetJump());
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.1f); // Adjust as needed
        isJumping = false;
        canMove = true; // Enable movement after finishing jump animation
    }

    void Crouch()
    {
        if (movementInput.y < 0 && !isCrouching && !isJumping)
        {
            isCrouching = true;
            anim.SetBool("Crouch", true);
            canMove = false; // Disable movement while crouching
        }
        else if (movementInput.y == 0 && isCrouching)
        {
            isCrouching = false;
            anim.SetBool("Crouch", false);
            canMove = true; // Enable movement after standing up from crouch
        }
    }

    void CheckScreenBounds()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x <= 20)
        {
            canWalkLeft = false;
        }
        else if (screenPos.x >= Screen.width - 20)
        {
            canWalkRight = false;
        }
        else
        {
            canWalkLeft = true;
            canWalkRight = true;
        }
    }
}
