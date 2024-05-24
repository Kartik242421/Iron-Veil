using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player1Action : MonoBehaviour
{
    public float JumpSpeed = 1f;
    public float FlipSpeed = 0.8f;

    public GameObject Player1;
    private Animator anim;

    private AnimatorStateInfo playerAnimatorState; // Animator state info

    public float PunchSlideAmt = 2f;
    private bool HeavyMoving = false;
    
    //audio
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh; 

    void Start()
    {
        anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();

    }
    void Update()
    {
        playerAnimatorState = anim.GetCurrentAnimatorStateInfo(0); // Get current animator state
        HeavyPunchSlideDirection();
        


    }


    public void HeavyPunchSlideDirection()
    {
        //Heavy Punch Slide
        if (HeavyMoving == true)
        {
            if (Player1Move_New.FacingRightP1 == true)
            {
                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player1Move_New.FacingLeftP1 == true)
            {
                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }

        }
    }
    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
        //Player1.transform.Translate(0, 0, 0);

    }
    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.05f);
        HeavyMoving = false;
    }


    //predefined function for attacking & jumping:-
    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
    }

    public void FlipUp()
    {
        Player1.transform.Translate(0, FlipSpeed, 0);
        //Player2.transform.Translate(0.1f , 0, 0);
    }

    public void FlipBack()
    {
        Player1.transform.Translate(0, FlipSpeed, 0);
        //Player2.transform.Translate(-0.1f , 0, 0);
    }

    //Sound:-
    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }
    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    //calling for input:-
    public void OnLightPunchEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0.5f) // Check if button is pressed (float value > 0.5)
        {
            LightPunch();
        }
    }

    public void OnHeavyPunchEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0.5f) // Check if button is pressed (float value > 0.5)
        {
            HeavyPunch();
        }
    }
    public void OnLightKickEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0.5f) // Check if button is pressed (float value > 0.5)
        {
            LightKick();
        }
    }
    public void OnHeavyKickEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() > 0.5f) // Check if button is pressed (float value > 0.5)
        {
            HeavyKick();
        }
    }

    //calling for animations:-
    void LightPunch()
    {
        if (playerAnimatorState.IsTag("Motion"))
        {
            anim.SetTrigger("LightPunch");
        }
    }

    void HeavyPunch()
    {
        if (playerAnimatorState.IsTag("Motion"))
        {
            anim.SetTrigger("HeavyPunch");
        }
    }

    void LightKick()
    {
        if (playerAnimatorState.IsTag("Motion") || playerAnimatorState.IsTag("Crouching"))
        {
            anim.SetTrigger("LightKick");
        }
    }
    void HeavyKick()
    {
        if (playerAnimatorState.IsTag("Motion") || playerAnimatorState.IsTag("Jumping"))
        {
            anim.SetTrigger("HeavyKick");
        }
    }

    
}
