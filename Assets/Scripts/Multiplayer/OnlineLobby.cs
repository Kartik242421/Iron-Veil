using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using TMPro; // Make sure to include TMPro namespace

public class OnlineLobby : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject playerPrefab; // Prefab for the player capsule
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points for players
    [SerializeField] private TMP_InputField joinCodeInputField; // Join code input field

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
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2, "asia-south1");
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log($"Allocation created {allocation.AllocationId}, {allocation.Region}, join code: {joinCode}");

            // Display or store the join code for the client to use
            joinCodeInputField.text = joinCode;

            LoadingScreen.SetActive(false);

            // Spawn host player after server is created
            SpawnPlayer();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Failed to create allocation: {e}");
            LoadingScreen.SetActive(false);
        }
    }

    public async void JoinAsClient(TMP_InputField joincode)
    {
        LoadingScreen.SetActive(true);
        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joincode.text);
            Debug.Log($"Allocation Joined {allocation.AllocationId}, {allocation.Region}");
            LoadingScreen.SetActive(false);
            Canvas.SetActive(false);

            // Spawn joining player after successful connection to server
            SpawnPlayer();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError($"Failed to join allocation: {e}");
            LoadingScreen.SetActive(false);
            Canvas.SetActive(true);
        }
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
