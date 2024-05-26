using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Trigger : MonoBehaviour
{
    public Collider col;
    public float damageAmt = 0.1f;
    public float PauseSpeed = 0.6f;

    public bool EmitFX = false;
    private ParticleSystem Particles;
    public string ParticleType = "P21";
    private GameObject ChosenParticles;

    public bool Player1 = true;

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        Particles = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1Action.Hits == false)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Player1 == true)
        {
            if (other.gameObject.CompareTag("Player2"))
            {
                if (EmitFX == true)
                {
                    Particles.Play();
                    Time.timeScale = PauseSpeed;
                }
                //col.enabled = true;
                Player1Action.Hits = true;
                SaveHealthData.Player2Health -= damageAmt;
                if (SaveHealthData.Player2Timer < 2.0f)
                {
                    SaveHealthData.Player2Timer += 2.0f;
                }

            }
        }
        else if (Player1 == false)
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                if (EmitFX == true)
                {
                    Particles.Play();
                    Time.timeScale = PauseSpeed;
                }
                //col.enabled = true;
                Player2Action.HitsP2 = true;
                SaveHealthData.Player1Health -= damageAmt;
                if (SaveHealthData.Player1Timer < 2.0f)
                {
                    SaveHealthData.Player1Timer += 2.0f;
                }

            }
        }
        
    }
}
