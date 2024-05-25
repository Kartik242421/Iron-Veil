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
        Debug.Log($"Allocation created {allocation.AllocationId},{allocation.Region}, {joincode}");
        LoadingScreen.SetActive(false);

    }
    public async void JoinAsClient(TMPro.TMP_InputField joincode)
    {
        LoadingScreen.SetActive(true);
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joincode.text);
        Debug.Log($"Allocation Joined {allocation.AllocationId}, {allocation.Region}");
        LoadingScreen.SetActive(true);
        Canvas.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }
}