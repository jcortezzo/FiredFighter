using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public AudioClip[] musics;
    public bool isPlayRandom;
    public static Jukebox Instance;

    private bool isMute;
    private AudioSource audioSource;

    public bool IsMute { get { return isMute; } }

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            if (this.musics[0].name != Instance.musics[0].name)
            {
                Instance.audioSource.volume = this.audioSource.volume;
                Instance.musics = this.musics;
                Instance.PlaySound();
            }
            ////instance.isMute = isMute;
            Destroy(this.gameObject);
            return;
        }
        

        

    }

    void Start()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        AudioClip audio = GetRandomAudioClip();
        //while (audioSource.clip != null && audio.name == audioSource.clip.name)
        //{
        //    audio = GetRandomAudioClip();
        //}
        audioSource.clip = audio;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < musics.Length)
        {
            AudioClip audio = musics[index];
            audioSource.clip = audio;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySound(string name)
    {
        AudioClip audio = Array.Find(musics, music => music.name == name);

        if (audio != null)
        {
            if (audioSource.clip != null && audioSource.clip.name == name)
            {
                Debug.LogWarning("Audio name: \"" + name + "\" is already played.");
                return;
            }
            audioSource.clip = audio;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio name: \"" + name + "\" is not found.");
        }
    }

    public void ToggleMusic()
    {
        audioSource.volume = audioSource.volume != 0 ? 0 : 1;
        isMute = !isMute;
    }

    private AudioClip GetRandomAudioClip()
    {
        return musics[UnityEngine.Random.Range(0, musics.Length)];
    }
}
