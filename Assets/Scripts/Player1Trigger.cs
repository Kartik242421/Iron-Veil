using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Trigger : MonoBehaviour
{
    public Collider col;
    public float damageAmt = 0.1f;
    // Update is called once per frame
    void Update()
    {
       /* if (Player1Actions.Hits == false)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }*/
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
           // Player1Actions.Hits = true;
            SaveHealthData.player2health -= damageAmt;
            if (SaveHealthData.player2Timer < 2.0f)
            {
                SaveHealthData.player2Timer += 2.0f;
            }
            
        }
        
    }
}
