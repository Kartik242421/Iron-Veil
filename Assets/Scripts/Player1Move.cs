using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Move : MonoBehaviour
{

    private Animator Anim;
    public float walkSpeed = 0.001f;
    private bool isJumping = false;
    private AnimatorStateInfo Player1Layer0;


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        transform.Translate(walkSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);


        //walking left and right

        if (Player1Layer0.IsTag("Motion"))
        {

            if (Input.GetAxis("Horizontal") > 0)
            {
                Anim.SetBool("Forward", true);
                transform.Translate(walkSpeed * Time.deltaTime, 0, 0);

            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Anim.SetBool("Backward", true);
                transform.Translate(-walkSpeed * Time.deltaTime, 0, 0);

            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }

        //Jumping and Crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if (isJumping == false)
            {
                isJumping = true;
                Anim.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }
    }
    
    
    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }
}

