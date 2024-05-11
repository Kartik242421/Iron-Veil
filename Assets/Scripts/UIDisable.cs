using UnityEngine;
using UnityEngine.SceneManagement;
public class UIDisable : MonoBehaviour
{
    public GameObject gameObjectToControl;

    void Start()
    {
        // Check if the platform is mobile
        bool isMobilePlatform = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        // Enable or disable the GameObject based on the platform
        gameObjectToControl.SetActive(isMobilePlatform);
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}