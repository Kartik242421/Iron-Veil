using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1JumpScript : MonoBehaviour
{
    public GameObject Player1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P2SpaceDetector"))
        {
            Player1.transform.Translate(-0.4f, 0, 0);
        }
    }
}


