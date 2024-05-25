using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class AssetLoading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        loadaddressables();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void loadaddressables()
    {
        int currentLevel = LevelSelect.CurrentLevel;
       // string assetKey = $"BG{currentLevel}";
        Addressables.LoadAssetAsync<GameObject>($"BG{currentLevel}").Completed += OnPlayerLoaded;
    }
    private void OnPlayerLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log(obj.Result.name);
        GameObject player = Instantiate(obj.Result);
       // player.transform.position = new Vector3(0, 1.12f, 8.84f);
       // player.transform.eulerAngles = new Vector3(0, 180, 0);
    }

}