using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject ball;
    public GameObject turnedBall;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
      
       
        
    }
    private void Update()
    {
        if (!LevelControllerLast.Current.gameActive)
            transform.position = new Vector3(transform.position.x, MazeRendererLast.Current.width * 2.5f, transform.position.z);
        else
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
            transform.position = Vector3.Slerp(transform.position, new Vector3(ball.transform.position.x, 6f, ball.transform.position.z), speed);
        }
        turnedBall.transform.position = new Vector3(transform.position.x, turnedBall.transform.position.y, (transform.position.y / 4) + 2.5f);
        float scale = (transform.position.y / 8f)-.5f;
        turnedBall.transform.localScale = new Vector3(scale, scale, scale);
    }
}
