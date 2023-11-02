using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioMixerGroup musicGroup;

    public void SetMusicVolume(float volume)
    {
        musicGroup.audioMixer.SetFloat("musicVol", volume);
    }
    public void SetMasterVolume(float volume)
    {
        musicGroup.audioMixer.SetFloat("masterVol", volume);
    }
}
