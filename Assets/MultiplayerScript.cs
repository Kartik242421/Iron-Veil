using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerScript : MonoBehaviour
{
    public int PlayerSceneIndex;
    public int OnlineSceneIndex;
    public int MainSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TwoPlayerOffline()
    {
        SaveHealthData.Player1Mode = false;
        SceneManager.LoadScene(PlayerSceneIndex);
    }
    public void TwoPlayerOnline()
    {
        SceneManager.LoadScene(OnlineSceneIndex);
    }
    public void Exit()
    {
        SceneManager.LoadScene(MainSceneIndex);
    }
}
