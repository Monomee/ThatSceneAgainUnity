using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider sliderMusic;
    [SerializeField] AudioMixer myAudioMixer;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void Start()
    {
        SetVolumeMusic();
    }
    public void SetVolumeMusic()
    {
        if(sliderMusic != null)
        {
            float volume = sliderMusic.value;
            myAudioMixer.SetFloat("Master", Mathf.Log10(volume)*20);
        }
    }
}
