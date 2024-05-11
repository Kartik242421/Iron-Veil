using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnePlayer()
    {
        SaveHealthData.Player1Mode = true;
        SceneManager.LoadScene(1);
    }
    public void TwoPlayer()
    {
        SaveHealthData.Player1Mode = false;
        SceneManager.LoadScene(1);

    }
    public void OptionScreen()
    {
        SceneManager.LoadScene(3);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ControlsScreen()
    {
        SceneManager.LoadScene(2);
    }
}
