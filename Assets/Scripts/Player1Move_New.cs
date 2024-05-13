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
    private AnimatorStateInfo playerAnimatorState; // Animator state info

    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;


    //public InputActionReference lightPunchAction;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        playerAnimatorState = anim.GetCurrentAnimatorStateInfo(0); // Get current animator state
        
        if (canMove)
        {
            Move();
        }
        Jump();
        Crouch();
        CheckScreenBounds();
        OppPositionMovement();
    }


    public void OppPositionMovement()
    {
        //get Opp position
        OppPosition = Opponent.transform.position;


        //facing left or right of the opponent
        if (OppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());

        }
        if (OppPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());

        }
    }

    public void OnMovementEvent (InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnLightPunchEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0.5f) // Check if button is pressed (float value > 0.5)
        {
            LightPunch();
        }
    }
    void LightPunch()
    {
        if (!isJumping && !isCrouching)
        {
            // Perform light punch animation
            anim.SetTrigger("LightPunch");

            // Perform any other actions related to light punch (e.g., dealing damage)
            // You need to implement this part based on your game's logic.
        }
    }

    void Move()
    {
        
        if (!isJumping && playerAnimatorState.IsTag("Motion")) // Only allow movement if not jumping and in motion state
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

    IEnumerator FaceRight()
    {
        if (FacingRight == true)
        {
            FacingRight = false;
            FacingLeft = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);

            anim.SetLayerWeight(1, 1);

        }
    }
    IEnumerator FaceLeft()
    {
        if (FacingLeft == true)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            anim.SetLayerWeight(1, 0);
        }
    }
}