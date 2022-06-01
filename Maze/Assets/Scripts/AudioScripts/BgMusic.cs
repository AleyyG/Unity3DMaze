using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    public static BgMusic Current;
    private void Awake()
    {

        if(Current != null && Current != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Current = this;
        DontDestroyOnLoad(this);
        GetBool("music");
    }
    public static void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }
    public static bool GetBool(string key)
    {
        int value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void MuteMusic()
    {
        if(GetBool("music"))
            transform.GetComponent<AudioSource>().mute = true;
    }
    public void NotMuteMusic()
    {
        if(!GetBool("music"))
            transform.GetComponent<AudioSource>().mute = false;
    }

    public void LowVolumeMusic()
    {
        transform.GetComponent<AudioSource>().volume = 0.1f;
    }
    public void HighVolumeMusic()
    {
        transform.GetComponent<AudioSource>().volume = 0.2f;
    }
}
