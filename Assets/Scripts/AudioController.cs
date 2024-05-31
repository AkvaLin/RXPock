using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource sounds;
    [SerializeField] private AudioSource music;
    public ReactiveProperty<bool> isMusicEnabled = new ReactiveProperty<bool>(true);
    public ReactiveProperty<bool> isSoundsEnabled = new ReactiveProperty<bool>(true);
    [CanBeNull] private static AudioController audioController = null;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        if (audioController == null) {
            audioController = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Subscribe();
    }

    public void Refresh()
    {
        isMusicEnabled = new ReactiveProperty<bool>(isMusicEnabled.Value);
        isSoundsEnabled = new ReactiveProperty<bool>(isSoundsEnabled.Value);
        Subscribe();
    }

    private void Subscribe()
    {
        isMusicEnabled.Subscribe(flag =>
        {
            if (flag)
            {
                music.Play();
            }
            else
            {
                music.Stop();
            }
        });

        isSoundsEnabled.Subscribe(flag =>
        {
            sounds.mute = !flag;
        });
    }
}
