using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Action : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public GameObject Player1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed * Time.deltaTime, 0);
        
    }
}
