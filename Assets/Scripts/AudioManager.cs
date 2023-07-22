using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioClip flip, destroy;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void play(bool isFlipped)
    {
        if (isFlipped)
        {
            GetComponent<AudioSource>().clip = flip;

        }
        else
        {
            GetComponent<AudioSource>().clip = destroy;

        }
        GetComponent<AudioSource>().Play();

    }
}
