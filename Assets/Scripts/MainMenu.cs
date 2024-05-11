using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int PlayerSceneIndex = 3;
    [SerializeField] private int optionScreenSceneIndex = 3;
    [SerializeField] private int controlsScreenSceneIndex = 2;

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
        SceneManager.LoadScene(PlayerSceneIndex);
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
}
