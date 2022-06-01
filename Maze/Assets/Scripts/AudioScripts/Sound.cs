using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    void Update()
    {
        SoundManager.Current.MuteSounds();
        SoundManager.Current.NotMuteSounds();
    }
}
