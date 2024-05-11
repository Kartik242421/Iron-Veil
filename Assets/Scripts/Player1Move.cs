using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Move : MonoBehaviour
{

    private Animator Anim;
    public float walkSpeed = 0.001f;
    public float JumpSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        transform.Translate(walkSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Check Player Knockout
        if (SaveHealthData.player1health <= 0)
        {
            Anim.SetTrigger("Knockout");
            Player1.GetComponet<Player1Actions>().enabled = false;
            this.GetComponent<Player1Move>().enabled = false;
        }
        //walking left and right
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
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }

        //Jumping and Crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            Anim.SetTrigger("Jump");
            transform.Translate(0, JumpSpeed * Time.deltaTime, 0);

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


    
}

