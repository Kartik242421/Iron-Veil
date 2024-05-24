using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player1object;
    public GameObject player2object;
    public Transform player1spawn;
    public Transform player2spawn;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player1object, player1spawn.position,player1spawn.rotation);
        Instantiate(player2object, player2spawn.position, player2spawn.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
