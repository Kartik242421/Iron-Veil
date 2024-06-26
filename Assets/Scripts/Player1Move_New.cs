using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Move_New : MonoBehaviour
{
    public float speed = 1f;
    private Animator anim;
    private Vector2 movementInput;
    public static bool isJumping = false;
    public static bool isCrouching = false;
    public static bool canMove = true; // Flag to control movement
    public static bool canWalkLeft = true; // Flag to control walking left
    public static bool canWalkRight = true; // Flag to control walking right
    private AnimatorStateInfo playerAnimatorState; // Animator state info

    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    public static bool FacingLeftP1 = false;
    public static bool FacingRightP1 = true;

    //sound
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;


    //public InputActionReference lightPunchAction;
    public InputActionReference blockAction;

    public static bool WalkRightP1 = true;
    public static bool WalkLeftP1 = true;

    public GameObject Restrict;

    public Rigidbody RB;
    public Collider BoxCollider;
    public Collider CapsuleCollider;
    public GameObject WinCondition;


    void Awake()
    {
        blockAction.action.performed += ctx => OnBlockEvent(ctx); // Subscribe to the block action
        blockAction.action.canceled += ctx => EndBlock(); // Subscribe to the block action cancellation
    }

    void OnEnable()
    {
        blockAction.action.Enable(); // Enable the block action
    }

    void OnDisable()
    {
        blockAction.action.Disable(); // Disable the block action
    }

    void Start()
    {
        WinCondition = GameObject.Find("WinCondition");
        WinCondition.gameObject.SetActive(false);
        Opponent = GameObject.Find("Player2");
        anim = GetComponentInChildren<Animator>();
        MyPlayer = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        //check if we are knocked out and play victory animation
        CheckKnockedOut();

        playerAnimatorState = anim.GetCurrentAnimatorStateInfo(0); // Get current animator state
        
        if (canMove)
        {
            Move();
        }
        Jump();
        Crouch();
        CheckScreenBounds();
        OppPositionMovement();

        // Reset the restrict
        if (Restrict.activeInHierarchy == false)
        {
            WalkLeftP1 = true;
            WalkRightP1 = true;
        }
        ColliderOnOff();
        
    }

    void ColliderOnOff()
    {
        if (playerAnimatorState.IsTag("Block"))
        {
            RB.isKinematic = true;
            BoxCollider.enabled = false;
            CapsuleCollider.enabled = false;
        }
        else
        {
            BoxCollider.enabled = true;
            CapsuleCollider.enabled = true;
            RB.isKinematic = false;
        }
    }

    void CheckKnockedOut()
    {
        if (SaveHealthData.Player1Health <= 0)
        {
            anim.SetTrigger("KnockOut");
            Player1.GetComponent<Player1Action>().enabled = false;
            StartCoroutine(KnockedOut());
            WinCondition.gameObject.SetActive(true);
            WinCondition.gameObject.GetComponent<WinLose>().enabled = true;
            //this.GetComponent<Player1Move_New>().enabled = false;
        }
        //Play victory animation
        if (SaveHealthData.Player2Health <= 0)
        {
            anim.SetTrigger("Victory");
            Player1.GetComponent<Player1Action>().enabled = false;
            this.GetComponent<Player1Move_New>().enabled = false;
            WinCondition.gameObject.SetActive(true);
            WinCondition.gameObject.GetComponent<WinLose>().enabled = true;
        }
    }

    //reaction+ Sounds
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("FistHeavy"))
        {
            anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            anim.SetTrigger("BigReact");
            MyPlayer.clip = HeavyKick;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickLight"))
        {
            anim.SetTrigger("HeadReact");
            MyPlayer.clip = LightKick;
            MyPlayer.Play();
        }
    }

    public void OnMovementEvent (InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

   

    void Move()
    {
        
        if (!isJumping && playerAnimatorState.IsTag("Motion")) // Only allow movement if not jumping and in motion state
        {
            Time.timeScale = 1.0f;
            float horizontalInput = movementInput.x;
            if (horizontalInput > 0 && canWalkRight) // Moving right
            {
                if (WalkRightP1 == true)  //collider checking to walk right
                {
                    anim.SetBool("Forward", true);
                    //anim.SetBool("Backward", false);
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
            }
            else if (horizontalInput < 0 && canWalkLeft) // Moving left
            {
                if (WalkLeftP1 == true)  // collider to not push
                {
                    //anim.SetBool("Forward", false);
                    anim.SetBool("Backward", true);
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
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
        yield return new WaitForSeconds(0.2f); // Adjust as needed
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


    IEnumerator FaceRight()
    {
        if (FacingRightP1 == true)
        {
            FacingRightP1 = false;
            FacingLeftP1 = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);

            anim.SetLayerWeight(1, 1);

        }
    }
    IEnumerator FaceLeft()
    {
        if (FacingLeftP1 == true)
        {
            FacingLeftP1 = false;
            FacingRightP1 = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            anim.SetLayerWeight(1, 0);
        }
    }

    public void OnBlockEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0.5f) // Check if button is pressed (float value > 0.5)
        {
            StartBlock();
        }
        else
        {
            EndBlock();
        }
    }

    //calling for animations:-
    void StartBlock()
    {
        // Start blocking animation or any other actions related to starting block
        anim.SetTrigger("BlockOn");
    }

    void EndBlock()
    {
        // End blocking animation or any other actions related to ending block
        anim.SetTrigger("BlockOff");
    }

    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player1Move_New>().enabled = false;
    }
}