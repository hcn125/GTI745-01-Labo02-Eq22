using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public static bool Change = false;
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
            Change = true;
        } 

        if (Change) {
            if (Paused == true) {
                Pause();
                Change = false;
            } else {
                Resume();
                Change = false;
            }
        }
    }
}
