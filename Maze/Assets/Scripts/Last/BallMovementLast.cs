using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BallMovementLast : MonoBehaviour
{

    public static BallMovementLast Current;
    private int speed;
    float vertical;
    float horizontal;
    public bool isFinished;
    public int speedValue;
    Rigidbody rb;
    public int heartCounter;
    public int starsCount;
    void Start()
    {
        Current = this;
        rb = GetComponent<Rigidbody>();
        starsCount = 3;
        
    }
    void Update()
    {
        if (LevelControllerLast.Current.gameActive)
        {
            HorizontalMovement();
            VerticalMovement();
        }
    }
    public void ChangeSpeed(int value)
    {
        speed = value;
    }

    void HorizontalMovement()
    {
        horizontal = Input.acceleration.x;
        
        if (horizontal > .15f && LevelControllerLast.Current.gameActive) //    (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)
        {
            SoundManager.Current.BallRolling();
            rb.velocity = Vector3.right * speed * Time.deltaTime;
        }
        else if (horizontal < -.15f && LevelControllerLast.Current.gameActive) // horizontal < -.25f (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
        {
            SoundManager.Current.BallRolling();
            rb.velocity = Vector3.left * speed * Time.deltaTime;
        }
            
    }
    void VerticalMovement()
    {
        vertical = Input.acceleration.y;
        
        if (vertical > .15f && LevelControllerLast.Current.gameActive) // (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)
        {
            SoundManager.Current.BallRolling();
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
        }
        else if (vertical < -.6f && LevelControllerLast.Current.gameActive) //    (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)
        {
            SoundManager.Current.BallRolling();
            rb.velocity = Vector3.back * speed * Time.deltaTime;
        }
               
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            isFinished = true;
            SoundManager.Current.Hole();
            BgMusic.Current.LowVolumeMusic();
            MazeRendererLast.Current.levelBar.value = MazeRendererLast.Current.levelBar.maxValue;
            LevelControllerLast.Current.FinishGame();
        }
        else if (other.tag == "Wall")
        {
            if (LevelControllerLast.Current.gameActive)
            {
                SoundManager.Current.HeartBreak();
                heartCounter++;
                starsCount--;
                if(heartCounter == 3)
                {
                    starsCount = 0;
                    LevelControllerLast.Current.gameOverMenu.SetActive(true);
                    rb.velocity = Vector3.zero;
                    LevelControllerLast.Current.gameActive = false;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hole")
            StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.35f);
        rb.velocity = Vector3.zero;
        
    }
}
