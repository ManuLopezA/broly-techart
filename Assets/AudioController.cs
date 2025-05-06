using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = audioClips[4];
        audioSource.Play();
    }

    public void PlaySound(int audioPosition)
    {
        audioSource.PlayOneShot(audioClips[audioPosition]);
    }
}
