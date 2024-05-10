using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Trigger : MonoBehaviour
{
    public Collider col;
    public float damageAmt = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (Player2Actions.HitsP2 == false)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            Player2Actions.HitsP2 = true;
            SaveHealthData.player1health -= damageAmt;
            if (SaveHealthData.player1Timer < 2.0f)
            {
                SaveHealthData.player1Timer += 2.0f;
            }

        }

    }
}
