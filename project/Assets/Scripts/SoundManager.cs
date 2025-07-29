using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundData[] sounds;
    [SerializeField] AudioMixerGroup sfxMixerGroup;
    [SerializeField] AudioMixerGroup musicMixerGroup;
    // Start is called once before the first execution of Update after the script is enabled
    void Start()
    {
        foreach (SoundData sound in sounds)
        {
            sound.myAudioSource = gameObject.AddComponent<AudioSource>();
            sound.myAudioSource.loop = sound.isLooping;
            sound.myAudioSource.playOnAwake = sound.isPlayOnAwake;
            sound.myAudioSource.clip = sound.soundClip;




            if (sound.isMusic)
            {
                sound.myAudioSource.outputAudioMixerGroup = musicMixerGroup;
            }
            else
            {
                sound.myAudioSource.outputAudioMixerGroup = sfxMixerGroup;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void PlaySound(string soundName)
    {
        foreach (SoundData sound in sounds)
        {
            if (sound.soundName == soundName)
            {
                sound.myAudioSource.Play();
            }
        }
    }
}
