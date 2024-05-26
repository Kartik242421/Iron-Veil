﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchOnP1 : MonoBehaviour
{
    public GameObject P1Character;
    public string P1Name = "Player 1";
    public TextMeshProUGUI P1Text;
    public GameObject VictoryScreen;
    public float WaitTime = 1.5f;
    private bool Victory = false;

    // Start is called before the first frame update
    void Start()
    {
        P1Text.text = P1Name;
        SaveHealthData.Player1Load = P1Character;
    }

    // Update is called once per frame
    void Update()
    {
        if(SaveHealthData.Player1Wins > 1)
        {
            if (Victory == false)
            {
                Victory = true;
                StartCoroutine(SetVictory());
            }
        }
        if(SaveHealthData.Player2Wins > 1)
        {
            if(Victory == false)
            {
                Victory = true;
                StartCoroutine(IconOff());
            }
        }
    }

    IEnumerator SetVictory()
    {
        yield return new WaitForSeconds(WaitTime);
        VictoryScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator IconOff()
    {
        yield return new WaitForSeconds(WaitTime);
    }
}
