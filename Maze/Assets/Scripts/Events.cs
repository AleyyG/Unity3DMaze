using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public static Events Current;
    public GameObject marketPanel;
    public GameObject startPanel;
    public GameObject ball;

    bool isMuteMusic = true;
    bool isMuteSound = true;
    public void OpenMarket()
    {
        ball.SetActive(true);
        marketPanel.SetActive(true);
        startPanel.SetActive(false);
        SoundManager.Current.Click();
    }
    public void CloseMarket()
    {
        ball.SetActive(false);
        marketPanel.SetActive(false);
        startPanel.SetActive(true);
        SoundManager.Current.Click();
    }
    public void Music()
    {
        if(isMuteMusic)
        {
            isMuteMusic = false;
            BgMusic.SetBool("music", true);
        }
        else
        {
            isMuteMusic = true;
            BgMusic.SetBool("music", false);
        }
    }
    public void Sound()
    {
        if (isMuteSound)
        {
            isMuteSound = false;
            SoundManager.SetBool("sound", true);
        }
        else
        {
            isMuteSound = true;
            BgMusic.SetBool("sound", false);
        }
    }

}
