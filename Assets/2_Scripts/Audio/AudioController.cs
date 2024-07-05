using Base.Data;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Toggle MusicToggle;
    [SerializeField] private AudioMixer musicMixer;

    private void Start()
    {
        MusicToggle.isOn = Data.Instance.SaveData.IsMusicOn;

        MusicToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool arg0)
    {
        int value = arg0 ? 0 : -80;
        musicMixer.SetFloat("Volume", value);
        
        Data.Instance.ChangeMusicVolume(arg0);
    }
}
