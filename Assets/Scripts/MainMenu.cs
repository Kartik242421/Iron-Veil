using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int PlayerSceneIndex = 3;
    public int Player2SceneIndex;
    public int optionScreenSceneIndex = 3;
    public int controlsScreenSceneIndex = 2;
    public int mainmenuIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnePlayer(int Scene)
    {
        SaveHealthData.Player1Mode = true;
        SceneManager.LoadScene(PlayerSceneIndex);
    }

    public void TwoPlayer()
    {
        SaveHealthData.Player1Mode = false;
        SceneManager.LoadScene(Player2SceneIndex);
    }

    public void OptionScreen()
    {
        SceneManager.LoadScene(optionScreenSceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ControlsScreen()
    {
        SceneManager.LoadScene(controlsScreenSceneIndex);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(mainmenuIndex);
    }
}
