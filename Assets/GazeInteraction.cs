using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GazeInteraction : MonoBehaviour
{
    public float gazeTime = 2f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void PointerEnter()
    {
        

        SceneManager.LoadScene(1);
        Debug.Log("PointerEnter");
        
       
    }

    

   
    public void QuitApp()
    {
        Application.Quit();
    }


}
