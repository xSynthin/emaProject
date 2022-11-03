using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UniversalClipSpeaker : MonoBehaviour
{
    public AudioSource src;

    public void PlayCLip(AudioClip clip)
    {
        src.clip = clip;
        src.Play();
        print(clip.length);
        Destroy(this.gameObject, clip.length);
    }
}
