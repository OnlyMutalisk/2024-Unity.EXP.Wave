using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    #region 싱글톤
    private static SoundManager _Instance;
    public static SoundManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
    }
    SoundManager() { }
    #endregion

    public AudioSource mainSound;
    public AudioSource noiseSound;
    public AudioSource effectSound;
    public AudioMixer bgmMixer;
    public AudioMixer sfxMixer;
    public AudioClip[] soundClips;
    public enum soundList
    {
        MaxCount, Track1, Track2, Track3, Track4, Track5, Track6, Track7, Track8, Track9, Track10, Track11, Track12,
        MainTheme1, MainTheme2, EndingTheme, EnterStage, ShoutSound, StompSound
    }
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button toggleBGM; // 배경음 음소거 버튼
    public Button toggleSFX; // 효과음 음소거 버튼

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    public void SlideControlBGM() // 배경음 슬라이드 조절
    {
        if (bgmSlider.value == -40)
        {
            bgmMixer.SetFloat("BGM", -80);
        }
        else
        {
            bgmMixer.SetFloat("BGM", bgmSlider.value);
        }
        PlayerPrefs.SetFloat("savedBGM", bgmSlider.value);
        PlayerPrefs.Save();
    }
    public void SlideControlSFX() // 배경음 슬라이드 조절
    {
        if (sfxSlider.value == -40)
        {
            sfxMixer.SetFloat("BGM", -80);
        }
        else
        {
            sfxMixer.SetFloat("BGM", sfxSlider.value);
        }
        PlayerPrefs.SetFloat("savedSFX", sfxSlider.value);
        PlayerPrefs.Save();
    }
    public void OnOffBGM() // 배경음 온오프
    {
        if (!mainSound.mute)
        {
            mainSound.mute = true;
            PlayerPrefs.SetInt("savedOnOffBGM", 0);
            PlayerPrefs.Save();
        }
        else
        {
            mainSound.mute = false;
            PlayerPrefs.SetInt("savedOnOffBGM", 1);
            PlayerPrefs.Save();
        }
    }
    public void OnOffSFX() // 배경음 온오프
    {
        if (!effectSound.mute)
        {
            effectSound.mute = true;
            PlayerPrefs.SetInt("savedOnOffSFX", 0);
            PlayerPrefs.Save();
        }
        else
        {
            effectSound.mute = false;
            PlayerPrefs.SetInt("savedOnOffSFX", 1);
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        DontDestroyOnLoad(_Instance.gameObject);
        DontDestroyOnLoad(_Instance.mainSound.gameObject);
        DontDestroyOnLoad(_Instance.effectSound.gameObject);

        for (int i = 0; i < (int)soundList.MaxCount; i++)
        {
            soundList soundName = (soundList)i;
            soundClips[i] = Resources.Load<AudioClip>($"Sounds/{soundName}");
        }

        if (PlayerPrefs.HasKey("savedVolumeBGM"))
        {
            bgmSlider.value = PlayerPrefs.GetFloat("savedVolumeBGM");
            sfxSlider.value = PlayerPrefs.GetFloat("savedVolumeSFX");
            mainSound.mute = PlayerPrefs.GetInt("savedOnOffBGM") == 0 ? true : false;
            effectSound.mute = PlayerPrefs.GetInt("savedOnOffSFX") == 0 ? true : false;
        }
        else
        {
            PlayerPrefs.SetFloat("savedVolumeBGM", bgmSlider.value);
            PlayerPrefs.SetFloat("savedVolumeSFX", sfxSlider.value);
            PlayerPrefs.SetInt("savedOnOffBGM", 1);
            PlayerPrefs.SetInt("savedOnOffSFX", 1);
            PlayerPrefs.Save();
        }
    }
}