using System.Globalization;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string soundName;

    public AudioClip soundClip;
    [Range(0f, 1f)]
    public float volume;

    public bool isLooping;

    public bool isPlayOnAwake;
    [Range(0f, 1f)]
    public bool isMusic;

    public AudioSource myAudioSource;
}