using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Trigger : MonoBehaviour
{
    public Collider col;
    public float damageAmt = 0.1f;
    //Update is called once per frame
    void Update()
    {
        if (Player2Action.HitsP2 == false)
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
        if (other.gameObject.CompareTag("Player1"))
        {
            //col.enabled = true;
            Player2Action.HitsP2 = true;
            SaveHealthData.Player1Health -= damageAmt;
            if (SaveHealthData.Player1Timer < 2.0f)
            {
                SaveHealthData.Player1Timer += 2.0f;
            }

        }

    }
}
