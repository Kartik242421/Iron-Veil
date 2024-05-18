using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2TriggerAI : MonoBehaviour
{
    public Collider Col;
    public float DamageAmt = 0.1f;

    public bool EmitFX = false;
    public ParticleSystem Particles;
    public float PauseSpeed = 0.6f;
    public string ParticleType = "P11";

    public bool Player2 = true;

    private GameObject ChosenParticles;

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        Particles = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Player2 == true)
        {
            if (Player2ActionsAI.HitsAI == false)
            {
                Col.enabled = true;
            }
            else
            {
                Col.enabled = false;
            }
        }
        else
        {
            if (Player1Actions.Hits == false)
            {
                Col.enabled = true;
            }
            else
            {
                Col.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SaveHealthData.P1Reacting == false)
        {
            if (Player2 == true)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (EmitFX == true)
                    {
                        Particles.Play();
                        Time.timeScale = PauseSpeed;
                    }
                    Player2ActionsAI.HitsAI = true;
                    SaveHealthData.Player1Health -= DamageAmt;
                    if (SaveHealthData.Player1Timer < 2.0f)
                    {
                        SaveHealthData.Player1Timer += 2.0f;
                    }
                }
            }
        }
        if (SaveHealthData.P2Reacting == false)
        {
            if (Player2 == false)
            {
                if (other.gameObject.CompareTag("Player2"))
                {
                    if (EmitFX == true)
                    {
                        Particles.Play();
                        Time.timeScale = PauseSpeed;
                    }
                    Player1Actions.Hits = true;
                    SaveHealthData.Player2Health -= DamageAmt;
                    if (SaveHealthData.Player2Timer < 2.0f)
                    {
                        SaveHealthData.Player2Timer += 2.0f;
                    }

                }
            }
        }
    }
}
