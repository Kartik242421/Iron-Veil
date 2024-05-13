using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManage : MonoBehaviour
{
   public void back()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(0);
    }
}
