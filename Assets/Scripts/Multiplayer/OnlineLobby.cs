using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

public class OnlineLobby : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject playerPrefab; // Prefab for the player capsule
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points for players

    private int currentPlayerIndex = 0; // Index to track current player

    // Start is called before the first frame update
    private void Awake()
    {
        Canvas.SetActive(true);
    }

    void Start()
    {
        LoadingScreen.SetActive(true);
        Initialize();
    }

    public async void Initialize()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        LoadingScreen.SetActive(false);
    }

    public async void StartAsHost()
    {
        LoadingScreen.SetActive(true);
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2, "asia-south1");
        string joincode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        Debug.Log($"Allocation created {allocation.AllocationId},{allocation.Region}");
        LoadingScreen.SetActive(false);
    }

    public async void JoinAsClient(TMPro.TMP_InputField joincode)
    {
        LoadingScreen.SetActive(true);
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joincode.text);
        Debug.Log($"Allocation Joined {allocation.AllocationId}, {allocation.Region}");
        LoadingScreen.SetActive(false);
        Canvas.SetActive(false);

        // Spawn player capsule after joining the server
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoints != null && spawnPoints.Length > 0)
        {
            // Use currentPlayerIndex to determine the spawn point for the player
            int spawnIndex = currentPlayerIndex % spawnPoints.Length;
            Instantiate(playerPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

            // Increment the player index for the next player
            currentPlayerIndex++;
        }
        else
        {
            Debug.LogError("Player prefab or spawn points not assigned or empty.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
