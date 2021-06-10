using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IMenu
{
    public List<GameObject> myGameObjects;
    public GameObject current;
    private int index = 0;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    public void Start()
    {
        current = myGameObjects[index];
    }

    public void Update()
    {
        EventSystem.current.SetSelectedGameObject(myGameObjects[index]);
    }

    public void next()
    {
        this.index++;
        if (this.index == myGameObjects.Count)
            this.index = 0;
        current = myGameObjects[index];
    }

    public void previous()
    {
        this.index--;
        if (this.index == -1)
            this.index = myGameObjects.Count - 1;
        current = myGameObjects[index];
    }
}