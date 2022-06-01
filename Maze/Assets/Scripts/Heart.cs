using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Sprite brokenheart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HeartSprite();
    }

    void HeartSprite()
    {
        if (BallMovement.Current.heartCounter == 1)
            heart3.sprite = brokenheart;
        if (BallMovement.Current.heartCounter == 2)
            heart2.sprite = brokenheart;
        if (BallMovement.Current.heartCounter == 3)
        {
            heart1.sprite = brokenheart;
        }
            
    }
}
