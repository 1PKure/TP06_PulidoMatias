using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("SliderPanel")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider fxSlider;
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioSource> sfxSources;
    [Range(0, 1)] [SerializeField] private float sfxVolume = 1.0f;
    [Range(0, 1)][SerializeField] private float masterVolume = 1.0f;
    [Range(0, 1)][SerializeField] private float fxVolume = 1.0f;
    private Dictionary<string, AudioClip> sfxDictionary;

    private void Awake()
    {
        fxSlider.onValueChanged.AddListener(SetFXVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        sfxDictionary = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
        {
            sfxDictionary[clip.name] = clip;
        }

        SetMasterVolume(masterVolume);
        SetSFXVolume(sfxVolume);
        SetFXVolume(sfxVolume);


    }
    public void PlaySFX(string clipName)
    {
        if (sfxDictionary.ContainsKey(clipName))
        {
            // Encuentra un AudioSource libre en la lista para reproducir el clip
            foreach (var source in sfxSources)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(sfxDictionary[clipName], sfxVolume);
                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("Clip no encontrado: " + clipName);
        }
    }
    private void OnDestroy()
    {
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        fxSlider.onValueChanged.RemoveListener(SetFXVolume);

    }

    public void SetSFXVolume(float volume)
    {
        fxVolume = volume;
        audioMixer.SetFloat("SFX", volume);

    }

    public void SetFXVolume(float volume)
    {
        sfxVolume = volume;
        audioMixer.SetFloat("FX", volume);

    }


    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        audioMixer.SetFloat("MasterVolume", volume);
    }
}

