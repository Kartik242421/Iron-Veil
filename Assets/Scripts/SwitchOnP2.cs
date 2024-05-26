﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchOnP2 : MonoBehaviour
{
    public GameObject P2Character;
    public GameObject AICharacter;
    public string P2Name = "Player 2";
    public TextMeshProUGUI P2Text;
    public GameObject VictoryScreen;
    public float WaitTime = 1.5f;
    private bool Victory = false;

    // Start is called before the first frame update
    void Start()
    {
        P2Text.text = P2Name;
        if (SaveHealthData.Player1Mode == false)
        {
            SaveHealthData.Player2Load = P2Character;
        }
        if (SaveHealthData.Player1Mode == true)
        {
            SaveHealthData.Player2Load = AICharacter;
        }
    }

    void Update()
    {
        if (SaveHealthData.Player2Wins > 1)
        {
            if (Victory == false)
            {
                Victory = true;
                StartCoroutine(SetVictory());
            }
        }
        if (SaveHealthData.Player1Wins > 1)
        {
            if (Victory == false)
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
