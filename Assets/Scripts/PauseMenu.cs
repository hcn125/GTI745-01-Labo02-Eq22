using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseUI;

    void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            Paused = !Paused;

            if (Paused == true)
            {
                Pause();

            }
            else
            {
                Resume();
            }

        }
        
    }
}
