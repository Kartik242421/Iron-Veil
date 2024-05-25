using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player2Action : MonoBehaviour
{
    public float JumpSpeed = 1f;
    public float FlipSpeed = 0.8f;

    public GameObject Player2;
    private Animator anim;

    private AnimatorStateInfo playerAnimatorState; // Animator state info
    private bool HeavyMoving = false;
    private bool HeavyReact = false;
    public float PunchSlideAmt = 2f;
    public float HeavyReactAmt = 4f;
    
    //audio
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;

    public static bool HitsP2 = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
    }
    void Update()
    {
        playerAnimatorState = anim.GetCurrentAnimatorStateInfo(0); // Get current animator state
        HeavyPunchSlideDirection();
        HeavyReactSlideDirection();
    }


    public void HeavyPunchSlideDirection()
    {
        //Heavy Punch Slide
        if (HeavyMoving == true)
        {
            if (Player2Move_New.FacingRightP2 == true)
            {
                Player2.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player2Move_New.FacingLeftP2 == true)
            {
                Player2.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }

        }
    }
    public void HeavyReactSlideDirection()
    {
        //Heavy React Slide
        if (HeavyReact == true)
        {
            if (Player2Move_New.FacingRightP2 == true)
            {
                Player2.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            if (Player2Move_New.FacingLeftP2 == true)
            {
                Player2.transform.Translate(+HeavyReactAmt * Time.deltaTime, 0, 0);
            }

        }
    }
    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
        //Player1.transform.Translate(0, 0, 0);
    }

    public void HeavyReaction()
    {
        StartCoroutine(HeavySlide());
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.05f);
        HeavyMoving = false;
    }

    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        yield return new WaitForSeconds(0.3f);
        HeavyReact = false;
    }


    //predefined function for attacking & jumping:-
    public void JumpUp()
    {
        Player2.transform.Translate(0, JumpSpeed , 0); 
    }

    public void FlipUp()
    {
        Player2.transform.Translate(0, FlipSpeed , 0);
        //Player2.transform.Translate(0.1f , 0, 0);
    }

    public void FlipBack()
    {
        Player2.transform.Translate(0, FlipSpeed , 0);
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
            HitsP2 = false;
        }
    }

    void HeavyPunch()
    {
        if (playerAnimatorState.IsTag("Motion"))
        {
            anim.SetTrigger("HeavyPunch");
            HitsP2 = false;
        }
    }

    void LightKick()
    {
        if (playerAnimatorState.IsTag("Motion") || playerAnimatorState.IsTag("Crouching"))
        {
            anim.SetTrigger("LightKick");
            HitsP2 = false;
        }
    }
    void HeavyKick()
    {
        if (playerAnimatorState.IsTag("Motion") || playerAnimatorState.IsTag("Jumping"))
        {
            anim.SetTrigger("HeavyKick");
            HitsP2 = false;
        }
    }

    
}
