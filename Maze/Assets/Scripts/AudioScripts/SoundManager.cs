using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Current;
    public AudioClip[] clapSounds;
    public AudioClip[] ballRolling;
    public AudioClip[] clicks;
    public AudioClip confetti;
    public AudioClip heartBreak;
    public AudioClip hole;


    static AudioSource audioSource;
    float timer;
    public float timeBetweenMove;
    // Start is called before the first frame update
    private void Awake()
    {

        if (Current != null && Current != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Current = this;
        DontDestroyOnLoad(this);
        GetBool("sound");
    } 
    void Start()
    {
        Current = this;
        audioSource = GetComponent<AudioSource>();

        confetti = Resources.Load<AudioClip>("Audio/confetti");
        heartBreak = Resources.Load<AudioClip>("Audio/heartBreak");
        hole = Resources.Load<AudioClip>("Audio/ballInHole");

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
    public void Click()
    {
        audioSource.clip = clicks[Random.Range(0, clicks.Length)];
        audioSource.Play();
    }
    public void Confetti()
    {
        audioSource.PlayOneShot(confetti);
    }
    public void HeartBreak()
    {
        audioSource.PlayOneShot(heartBreak);
    }

    public void Hole()
    {
        audioSource.PlayOneShot(hole);
    }
    public void ClapSounds()
    {
        audioSource.clip = clapSounds[Random.Range(0, clapSounds.Length)];
        audioSource.Play();
    }

    public void BallRolling()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = timeBetweenMove;
            audioSource.clip = ballRolling[Random.Range(0, ballRolling.Length)];
            audioSource.Play();
        }
    }

    public void MuteSounds()
    {
        if(GetBool("sound"))
            transform.GetComponent<AudioSource>().mute = true;
    }

    public void NotMuteSounds()
    {
        if(!GetBool("sound"))
            transform.GetComponent<AudioSource>().mute = false;
    }
}
