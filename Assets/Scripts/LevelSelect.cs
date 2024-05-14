using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject LevelBG1;
    public GameObject LevelBG2;
    public GameObject LevelBG3;
    public GameObject LevelBG4;
    public GameObject LevelBG5;
    public GameObject LevelBG6;
    public GameObject LevelBG7;
    public GameObject LevelBG8;
    public GameObject LevelBG9;
    public GameObject LevelBG10;
    private int MaxLevels = 10;
    private int CurrentLevel = 1;
    private float PauseTime = 0.4f;
    private bool TimeCountDown = false;
    public GameObject leftbutton;
    public GameObject rightbutton;
    public int Scene;
    // Start is called before the first frame update
    void Start()
    {
        LevelBG1.gameObject.SetActive(true);
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (TimeCountDown == true)
        {
            if (PauseTime > 0.1f)
            {
                PauseTime -= 0.4f * Time.deltaTime;
            }
            if (PauseTime <= 0.1f)
            {
                PauseTime = 0.4f;
                TimeCountDown = false;
            }
        }
            if (CurrentLevel == 1)
            {
                SwitchOff();
                LevelBG1.gameObject.SetActive(true);
                leftbutton.SetActive(false);
            }
            if (CurrentLevel == 2)
            {
                leftbutton.SetActive(true);
                SwitchOff();
                LevelBG2.gameObject.SetActive(true);
            }
            if (CurrentLevel == 3)
            {
                SwitchOff();
                LevelBG3.gameObject.SetActive(true);
            }
            if (CurrentLevel == 4)
            {
                SwitchOff();
                LevelBG4.gameObject.SetActive(true);
            }
            if (CurrentLevel == 5)
            {
                SwitchOff();
                LevelBG5.gameObject.SetActive(true);
            }
            if (CurrentLevel == 6)
            {
                SwitchOff();
                LevelBG6.gameObject.SetActive(true);
            }
            if (CurrentLevel == 7)
            {
                SwitchOff();
                LevelBG7.gameObject.SetActive(true);
            }
            if (CurrentLevel == 8)
            {
                SwitchOff();
                LevelBG8.gameObject.SetActive(true);
            }
            if (CurrentLevel == 9)
            {
                SwitchOff();
                LevelBG9.gameObject.SetActive(true);
                rightbutton.SetActive(true);
            }
            if (CurrentLevel == 10)
            {
                SwitchOff();
                LevelBG10.gameObject.SetActive(true);
                rightbutton.SetActive(false);
            }

    }

    public void Right()
    {
        if (PauseTime == 0.4f)
        {
            if (CurrentLevel <= MaxLevels)
            {
                CurrentLevel++;
                TimeCountDown = true;
            }
        }

    }
    public void Left()
    {
        if (PauseTime == 0.4f)
        {
            if (CurrentLevel >= 1)
            {
                CurrentLevel--;
                TimeCountDown = true;
            }
        }

    }
    public void choose()
    {
        Debug.Log("Choosen");
            SaveHealthData.LevelNumber = CurrentLevel;
            SceneManager.LoadScene(Scene);
    }
    void SwitchOff()
    {
        LevelBG1.gameObject.SetActive(false);
        LevelBG2.gameObject.SetActive(false);
        LevelBG3.gameObject.SetActive(false);
        LevelBG4.gameObject.SetActive(false);
        LevelBG5.gameObject.SetActive(false);
        LevelBG6.gameObject.SetActive(false);
        LevelBG7.gameObject.SetActive(false);
        LevelBG8.gameObject.SetActive(false);
        LevelBG9.gameObject.SetActive(false);
        LevelBG10.gameObject.SetActive(false);
    }
}
