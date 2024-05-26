using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Move_NewAI : MonoBehaviour
{
    public float WalkSpeed = 1f;
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
    public static bool FacingLeftAI = false;
    public static bool FacingRightAI = true;

    //sound
    public AudioClip LightPunch;
    public AudioClip HeavyPunch;
    public AudioClip LightKick;
    public AudioClip HeavyKick;
    private AudioSource MyPlayer;


    //public InputActionReference lightPunchAction;
    public InputActionReference blockAction;

    public static bool WalkRightAI = true;
    public static bool WalkLeftAI = true;

    public GameObject Restrict;

    public Rigidbody RB;
    public Collider BoxCollider;
    public Collider CapsuleCollider;

    private float OppDistance;
    public float AttackDistance = 1.5f;
    private bool MoveAI = true;
    public static bool AttackState = false;

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
        Opponent = GameObject.Find("Player1");
        anim = GetComponentInChildren<Animator>();
        MyPlayer = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        //calculate distance
        OppDistance = Vector3.Distance(Opponent.transform.position, Player1.transform.position);


        CheckKnockedOut();
        playerAnimatorState = anim.GetCurrentAnimatorStateInfo(0); // Get current animator state
        
        /*
        if (canMove)
        {
            Move();
        }*/

        Jump();
        Crouch();
        CheckScreenBounds();
        OppPositionMovementAndAIMovement();

        // Reset the restrict
        if (Restrict.activeInHierarchy == false)
        {
            WalkLeftAI = true;
            WalkRightAI = true;
        }
        ColliderOnOff();
    }

    public void OppPositionMovementAndAIMovement()
    {
        //get Opp position
        OppPosition = Opponent.transform.position;

        
        //facing left or right of the opponent
        if (OppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
            if (playerAnimatorState.IsTag("Motion")) // Only allow movement if not jumping and in motion state
            {
                Time.timeScale = 1.0f;
                anim.SetBool("CanAttack", false); //disable attacking
                //moving AI
                if (OppDistance > AttackDistance && canWalkRight) // Moving right
                {
                    if (MoveAI == true)
                    {
                        if (WalkRightAI == true)  //collider checking to walk right
                        {
                            anim.SetBool("Forward", true);
                            anim.SetBool("Backward", false);
                            AttackState = false;
                            transform.Translate(WalkSpeed * Time.deltaTime * Vector3.right);
                        }
                    }
                }
                if(OppDistance < AttackDistance && canWalkRight)
                {
                    if (MoveAI == true)
                    {
                        MoveAI = false;
                        anim.SetBool("Forward", false);
                        anim.SetBool("Backward", false);
                        anim.SetBool("CanAttack", true); //enables attacking

                        StartCoroutine(ForwardFalse());
                    }
                }
               
            }

        }
        if (OppPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
            //moving AI
            if (playerAnimatorState.IsTag("Motion")) // Only allow movement if not jumping and in motion state
            {
                Time.timeScale = 1.0f;
                anim.SetBool("CanAttack", false);  //set attacking to false

                if (OppDistance > AttackDistance && canWalkLeft) // Moving right
                {
                    if (MoveAI == true)
                    {
                        if (WalkLeftAI == true)  //collider checking to walk right
                        {
                            anim.SetBool("Backward", true);
                            anim.SetBool("Forward", false);
                            AttackState = false;

                            transform.Translate(-WalkSpeed * Time.deltaTime * Vector3.right);
                        }
                    }
                }
                if (OppDistance < AttackDistance && canWalkLeft)
                {
                    if (MoveAI == true)
                    {
                        MoveAI = false;
                        anim.SetBool("Forward", false);
                        anim.SetBool("Backward", false);
                        anim.SetBool("CanAttack", true); //set attacking true

                        StartCoroutine(ForwardFalse());
                    }
                }
            }
        }
    }

    IEnumerator ForwardFalse()
    {
        yield return new WaitForSeconds(0.6f);
        MoveAI = true;
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
        if (SaveHealthData.Player2Health <= 0)
        {
            anim.SetTrigger("KnockOut");
            Player1.GetComponent<Player2ActionAI>().enabled = false;
            StartCoroutine(KnockedOut());
        }
        if (SaveHealthData.Player1Health <= 0)
        {
            anim.SetTrigger("Victory");
            Player1.GetComponent<Player2ActionAI>().enabled = false;
            this.GetComponent<Player2Move_NewAI>().enabled = false;
        }
    }


    //reaction
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
                if (WalkRightAI == true)  //collider checking to walk right
                {
                    anim.SetBool("Forward", true);
                    //anim.SetBool("Backward", false);
                    transform.Translate(Vector3.right * WalkSpeed * Time.deltaTime);
                }
            }
            else if (horizontalInput < 0 && canWalkLeft) // Moving left
            {
                if (WalkLeftAI == true)  // collider to not push
                {
                    //anim.SetBool("Forward", false);
                    anim.SetBool("Backward", true);
                    transform.Translate(Vector3.left * WalkSpeed * Time.deltaTime);
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

    


    IEnumerator FaceRight()
    {
        if (FacingRightAI == true)
        {
            FacingRightAI = false;
            FacingLeftAI = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);

            anim.SetLayerWeight(1, 1);

        }
    }
    IEnumerator FaceLeft()
    {
        if (FacingLeftAI == true)
        {
            FacingLeftAI = false;
            FacingRightAI = true;
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
        this.GetComponent<Player2Move_NewAI>().enabled = false;
    }
}