using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //sound
    public AudioSource coinSource;
    public AudioSource wallSource;

    //elements
    public TextMeshProUGUI ballSpeedText;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI hpText;
    public GameObject winTextObject;

    //second level wall
    public GameObject BridgeLevel;
    public GameObject BlockLevelWall;
    public GameObject BlockLevelWallSide1;
    public GameObject BlockLevelWallSide2;

    //mouvement
    public float speed = 0;
    private int count;
    private int ballLeft = 11 ;
    private int currentLevel = 1;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int hp = 0;

    //countdown
    float currentTime = 0f;
    float startingTime = 20f;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        SetBallSpeedText();
        winTextObject.SetActive(false);


    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() {
        countText.text = "Count : " + count.ToString() + " Pickup left : " + ballLeft;


        if (count > 10 && currentLevel == 1)
        {
            currentLevel++;
            ballLeft = 8;
            count = 0;
            currentTime = 30.0f;
            countText.text = "Count : " + count.ToString() + " Pickup left : " + ballLeft;
            BridgeLevel.SetActive(true);            
            BlockLevelWallSide1.SetActive(true);
            BlockLevelWallSide2.SetActive(true);
            BlockLevelWall.SetActive(false);
        }
        else if (count > 7 && currentLevel == 2) {
            winTextObject.SetActive(true);
        }


    }

    void SetHpText()
    {
        if (hp < 0)
        {
            EndGame();
        }
        else {
            hpText.text = "Hp : " + hp.ToString() + " left";
        }
    }

    void SetBallSpeedText() {

        float ballVelocity = (float)System.Math.Round(rb.velocity.magnitude, 1);        
        ballSpeedText.text = "Ball speed : " + ballVelocity + "m/s";
    }

    void FixedUpdate()
    {   
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        
        SetBallSpeedText();
        countDownUpdate();
        SetHpText();
    }

    private void countDownUpdate()
    {
        if (currentTime >= 0)
        {
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = "Count down: " + currentTime.ToString("0");
        }
        else {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            coinSource.Play();
            other.gameObject.SetActive(false);
            count++;
            ballLeft--;

            SetCountText();
        }

        if (other.gameObject.CompareTag("PickUp2"))
        {
            coinSource.Play();
            other.gameObject.SetActive(false);
            count++;
            ballLeft--;

            SetCountText();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Walls")) {
            wallSource.Play();
        } else if (other.gameObject.CompareTag("Pendulum")) {
            wallSource.Play();
            hp--;
        }
    }
}
