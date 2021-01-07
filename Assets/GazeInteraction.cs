using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GazeInteraction : MonoBehaviour
{
    public float gazeTime = 2f;
    private float timer;
    private bool gazeAt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gazeAt)
        {
            timer += Time.deltaTime;
            if(timer >= gazeTime)
            {
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current),ExecuteEvents.pointerDownHandler);
                timer = 0f;
            }
        }
    }

    public void PointerEnter()
    {
        gazeAt = true;
        Debug.Log("PointerEnter");
    }

    public void PointerExit()
    {
        gazeAt = false;
        Debug.Log("PointerExit");
    }
    public void PointerDown()
    {
        Debug.Log("PointerDown");
    }

    public void StartFunc()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitApp()
    {
        Application.Quit();
    }


}
