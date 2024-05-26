using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;
    private GameObject Player1Character;
    private GameObject Player2Character;
    public Transform Player1Spawn;
    public Transform Player2Spawn;
    private AudioSource MyPlayer;

    public AudioClip Music1;
    public AudioClip Music2;
    public AudioClip Music3;
    public AudioClip Music4;
    public AudioClip Music5;
    public AudioClip Music6;
    public AudioClip Music7;
    public AudioClip Music8;
    public AudioClip Music9;
    public AudioClip Music10;

    public int Scene = 0;

    // Start is called before the first frame update
    void Start()
    {
        MyPlayer = GetComponent<AudioSource>();
        if (SaveHealthData.LevelNumber == 1)
        {
            MyPlayer.clip = Music1;
        }
        if (SaveHealthData.LevelNumber == 2)
        {
            MyPlayer.clip = Music2;
        }
        if (SaveHealthData.LevelNumber == 3)
        {
            MyPlayer.clip = Music3;
        }
        if (SaveHealthData.LevelNumber == 4)
        {
            MyPlayer.clip = Music4;
        }
        if (SaveHealthData.LevelNumber == 5)
        {
            MyPlayer.clip = Music5;
        }
        if (SaveHealthData.LevelNumber == 6)
        {
            MyPlayer.clip = Music6;
        }
        if (SaveHealthData.LevelNumber == 7)
        {
            MyPlayer.clip = Music7;
        }
        if (SaveHealthData.LevelNumber == 8)
        {
            MyPlayer.clip = Music8;
        }
        if (SaveHealthData.LevelNumber == 9)
        {
            MyPlayer.clip = Music9;
        }
        if (SaveHealthData.LevelNumber == 10)
        {
            MyPlayer.clip = Music10;
        }

        Player1 = GameObject.Find(SaveHealthData.P1Select);
        Debug.Log("Player1"+SaveHealthData.P1Select);
        Player1.gameObject.GetComponent<SwitchOnP1>().enabled = true;
        Player2 = GameObject.Find(SaveHealthData.P2Select);
        Debug.Log("Player2"+SaveHealthData.P2Select);
        Player2.gameObject.GetComponent<SwitchOnP2>().enabled = true;
        StartCoroutine(SpawnPlayers());

    }

    IEnumerator SpawnPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        Player1Character = SaveHealthData.Player1Load;
        Player2Character = SaveHealthData.Player2Load;
        Instantiate(Player1Character, Player1Spawn.position, Player1Spawn.rotation);
        Instantiate(Player2Character, Player2Spawn.position, Player2Spawn.rotation);
    }

    public void BackToSelection()
    {
        SceneManager.LoadScene(Scene);
    }
}
