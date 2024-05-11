using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Move_New : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 movementInput;
    private Animator anim;
    private bool isJumping = false;
    private bool isCrouching = false;
    private bool canMove = true; // Flag to control movement

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
    }

    public void OnMovementEvent(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    void Move()
    {
        if (!isJumping) // Only allow movement if not jumping
        {
            Vector3 movement = new Vector3(movementInput.x, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);

            // Check if the player is within screen bounds
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPos.x <= 20 && movementInput.x < 0) // Left bound
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(20, screenPos.y, screenPos.z));
            }
            else if (screenPos.x >= Screen.width - 20 && movementInput.x > 0) // Right bound
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 20, screenPos.y, screenPos.z));
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
}
