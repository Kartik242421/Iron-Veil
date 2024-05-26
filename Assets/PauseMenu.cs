using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pausemenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                pause();
            }

        }
    }
    public void pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(4);
        Time.timeScale = 1f;
        pausemenu.SetActive(false);
    }
    public void Exit()
    {
        
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        pausemenu.SetActive(false);
    }
}
