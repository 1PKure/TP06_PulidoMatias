using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Audio;

public class UISettings : MonoBehaviour
{
    [Header("SliderPanel")]
    //[SerializeField] private Slider player1SpeedSlider;
    [SerializeField] private Slider volumenSlider;
    [Header("UIText")]
    //[SerializeField] private TextMeshProUGUI player1SpeedText;
    //[Header("PlayerMovement")]
    //[SerializeField] private PlayerController player1movement;
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;
    private PlayerData playerData;

    private void Awake()
    {
        //player1SpeedSlider.onValueChanged.AddListener(SetPlayer1Speed);
        volumenSlider.onValueChanged.AddListener(SetVolume);

    }

    private void OnDestroy()
    {

        //player1SpeedSlider.onValueChanged.RemoveListener(SetPlayer1Speed);
        volumenSlider.onValueChanged.RemoveListener(SetVolume);


    }
    //public void SetPlayer1Speed(float speed)
    //{
    //    player1movement.SetMovementSpeed(speed);
    //    player1SpeedText.text = speed.ToString("F2");
    //}

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}

