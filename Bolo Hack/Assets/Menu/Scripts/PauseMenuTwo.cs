using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuTwo : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //if (isPaused)
        //{
            //Resume();
        //}
        //else
        //{
        //    Pause();
        //}
        
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Pause")
                {
                   Pause();
                }
                else {
                       
                }
            }
        } 
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
