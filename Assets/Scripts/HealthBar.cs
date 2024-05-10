using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Player1Green;
    public Image Player2Green;
    public Image Player1Red;
    public Image Player2Red;
    // public SaveHealthData SaveHealthData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player1Green.fillAmount = SaveHealthData.player1health;
        Player2Green.fillAmount = SaveHealthData.player2health;
        
        if (SaveHealthData.player2Timer > 0)
        {
            SaveHealthData.player2Timer-= 2.0f * Time.deltaTime;
        }

        if (SaveHealthData.player2Timer <= 0)
        {
            if (Player2Red.fillAmount > SaveHealthData.player2health)
            {
                Player2Red.fillAmount -= 0.003f;

            }
        }
        if (SaveHealthData.player1Timer > 0)
        {
            SaveHealthData.player1Timer -= 2.0f * Time.deltaTime;
        }

        if (SaveHealthData.player1Timer <= 0)
        {
            if (Player1Red.fillAmount > SaveHealthData.player1health)
            {
                Player1Red.fillAmount -= 0.003f;

            }
        }
    }
}
