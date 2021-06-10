using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumeBackground(float volume)
    {
        audioMixer.SetFloat("volumeBackGround", volume);
    }
    public void SetVolumeEffects(float volume)
    {
        audioMixer.SetFloat("volumeEffects", volume);
    }
}
