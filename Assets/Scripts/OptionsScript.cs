using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{
    public Slider Music;
    public Slider SFX;
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;

    // Start is called before the first frame update
    void Start()
    {
        Music.value = SaveHealthData.MusicVol;
        SFX.value = SaveHealthData.SFXVol;
        MusicMixer.SetFloat("MusicLevel", SaveHealthData.MusicVol);
        SFXMixer.SetFloat("SFXLevel", SaveHealthData.SFXVol);
    }

    public void DifficultyEasy()
    {
        SaveHealthData.DifficultyAmt = 3.0f;
    }

    public void DifficultyStandard()
    {
        SaveHealthData.DifficultyAmt = 2.0f;
    }

    public void DifficultyExpert()
    {
        SaveHealthData.DifficultyAmt = 1.0f;
    }

    public void MusicVolume()
    {
        MusicMixer.SetFloat("MusicLevel", Music.value);
        SaveHealthData.MusicVol = Music.value;
    }

    public void SFXVolume()
    {
        SFXMixer.SetFloat("SFXLevel", SFX.value);
        SaveHealthData.SFXVol = SFX.value;
    }
}
