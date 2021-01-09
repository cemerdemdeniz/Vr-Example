using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GazeInteraction : MonoBehaviour
{
   
   

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
