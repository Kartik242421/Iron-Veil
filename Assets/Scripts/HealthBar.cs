using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class HealthBar : MonoBehaviour
{
    public Image Player1Green;
    public Image Player2Green;
    public Image Player1Red;
    public Image Player2Red;
    public TextMeshProUGUI TimerText;
    public float LevelTime = 90;
    public GameObject WinCondition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveHealthData.TimeOut == false)
        {
            if (LevelTime > 0)
        {
            LevelTime -= 1* Time.deltaTime;
        }
        if (LevelTime <= 0.1)
        {
            SaveHealthData.TimeOut = true;
            WinCondition.gameObject.SetActive(true);
            WinCondition.gameObject.GetComponent<WinLose>().enabled=true;
        }

        }
        TimerText.text = Mathf.Round(LevelTime).ToString();
        
        Player1Green.fillAmount = SaveHealthData.Player1Health;
        Player2Green.fillAmount = SaveHealthData.Player2Health;
        
        if (SaveHealthData.Player2Timer > 0)
        {
            SaveHealthData.Player2Timer -= 2.0f * Time.deltaTime;
        }

        if (SaveHealthData.Player2Timer <= 0)
        {
            if (Player2Red.fillAmount > SaveHealthData.Player2Health)
            {
                Player2Red.fillAmount -= 0.003f;

            }
        }
        if (SaveHealthData.Player1Timer > 0)
        {
            SaveHealthData.Player1Timer -= 2.0f * Time.deltaTime;
        }

        if (SaveHealthData.Player1Timer <= 0)
        {
            if (Player1Red.fillAmount > SaveHealthData.Player1Health)
            {
                Player1Red.fillAmount -= 0.003f;

            }
        }
    }
}
