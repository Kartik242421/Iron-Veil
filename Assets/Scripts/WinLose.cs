using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Player1WinText;
    public GameObject Player2WinText;
    public AudioSource MyPlayer;
    public AudioClip LoseAudio;
    public AudioClip Player1WindAudio;
    public AudioClip Player2WindAudio;
    public float PauseTime = 4.0f;

    private int Scene = 3;

    // Start is called before the first frame update
    void Start()
    {
        SaveHealthData.TimeOut = false;
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        Player1WinText.gameObject.SetActive(false);
        Player2WinText.gameObject.SetActive(false);
        StartCoroutine(WinSet());
    }

    IEnumerator WinSet()
    {
        yield return new WaitForSeconds(0.4f);
        if (SaveHealthData.Player1Health > SaveHealthData.Player2Health)
        {
            if (SaveHealthData.Player1Mode == true)
            {
                WinText.gameObject.SetActive(true);
                MyPlayer.Play();
                SaveHealthData.Player1Wins++;
                Debug.Log(SaveHealthData.Player1Wins);
            }
            else if (SaveHealthData.Player1Mode == false)
            {
                Player1WinText.gameObject.SetActive(true);
                MyPlayer.clip = Player1WindAudio;
                MyPlayer.Play();
                SaveHealthData.Player1Wins++;
            }
        }
        else if (SaveHealthData.Player2Health > SaveHealthData.Player1Health)
        {
            if (SaveHealthData.Player1Mode == true)
            {
                LoseText.gameObject.SetActive(true);
                MyPlayer.clip = LoseAudio;
                MyPlayer.Play();
                SaveHealthData.Player2Wins++;
            }
            else if (SaveHealthData.Player1Mode == false)
            {
                Player2WinText.gameObject.SetActive(true);
                MyPlayer.clip = Player2WindAudio;
                MyPlayer.Play();
                SaveHealthData.Player2Wins++;
            }
        }
        yield return new WaitForSeconds(PauseTime);
        SceneManager.LoadScene(Scene);


    }
}
