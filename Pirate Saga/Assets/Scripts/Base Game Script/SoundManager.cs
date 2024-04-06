using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource[] destroyNoise;
    [SerializeField] private AudioSource backgroundMusic;

   private void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                backgroundMusic.Play();
                backgroundMusic.volume = 0;
            }
            else
            {
                backgroundMusic.Play();
                backgroundMusic.volume = 1;
            }
        }
        else
        {
            backgroundMusic.Play();
            backgroundMusic.volume = 1;
        }
    }

    public void AdjustVolume()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                backgroundMusic.volume = 0;            }
            else
            {
                backgroundMusic.volume = 1;
            }
        }
    }

    public void PlayRandomDestroyNoise()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                //Choose a random number
                int clipToPlay = Random.Range(0, destroyNoise.Length);
                //play that clip
                destroyNoise[clipToPlay].Play();
            }
        }
        else
        {
            //Choose a random number
            int clipToPlay = Random.Range(0, destroyNoise.Length);
            //play that clip
            destroyNoise[clipToPlay].Play();

        }
    }
}
