using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    public float distance = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
     transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;

       



        
    }
}
