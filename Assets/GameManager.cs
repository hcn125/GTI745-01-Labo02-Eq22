using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using NativeWebSocket;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject pauseMenu;
    public MainMenu mainMenu;
    public Rigidbody ballRB;
    public string messageWB;
    WebSocket websocket;

    // Start is called before the first frame update
    async void Start()
    {
        ball = GameObject.Find("Player");
        ballRB = ball.GetComponent<Rigidbody>();

        websocket = new WebSocket("ws://localhost:8080");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            //Debug.Log("OnMessage!");
            //Debug.Log(bytes);

            // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            messageWB = message;
            //Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
        if (!string.IsNullOrEmpty(messageWB))
        {
            //Debug.Log(messageWB);

            switch (messageWB)
            {
                case "Right":
                    if (!PauseMenu.Paused) {
                        ballRB.AddForce(Vector3.right * 2);
                    }
                    break;
                case "Left":
                    if (!PauseMenu.Paused) {
                        ballRB.AddForce(Vector3.left * 2);
                    }
                    break;
                case "Front":
                    if (!PauseMenu.Paused) {
                        ballRB.AddForce(Vector3.forward * 2);
                    }
                    break;
                case "Back":
                    if (!PauseMenu.Paused) {
                        ballRB.AddForce(Vector3.back * 2);
                    }
                    break;
                case "Ŝtop":
                    if (!PauseMenu.Paused)
                    {
                        ballRB.velocity = Vector3.zero;
                    }
                    break;
                case "Pause":
                    if (!PauseMenu.Paused) {
                        PauseMenu.Paused = true;
                        PauseMenu.Change = true;
                    }
                    break;
                case "Unpause":
                    if (PauseMenu.Paused) {
                        PauseMenu.Paused = false;
                        PauseMenu.Change = true;
                    }
                    break;
                case "Up":
                    pauseMenu = GameObject.Find("PauseMenu");
                    if (PauseMenu.Paused)
                    {
                        if (pauseMenu != null)
                        {
                            mainMenu = pauseMenu.GetComponent<MainMenu>();
                            mainMenu.previous();
                            System.Threading.Thread.Sleep(200);
                        }
                    }
                    break;
                case "Down":
                    pauseMenu = GameObject.Find("PauseMenu");
                    if (PauseMenu.Paused)
                    {
                        if (pauseMenu != null)
                        {
                            mainMenu = pauseMenu.GetComponent<MainMenu>();
                            mainMenu.next();
                            System.Threading.Thread.Sleep(200);
                        }
                    }
                    break;
                case "Click":
                    if (PauseMenu.Paused)
                    {
                        pauseMenu = GameObject.Find("PauseMenu");
                        if (pauseMenu != null)
                        {
                            mainMenu = pauseMenu.GetComponent<MainMenu>();
                            mainMenu.current.GetComponent<Button>().onClick.Invoke();
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                    break;
            }
        }
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}