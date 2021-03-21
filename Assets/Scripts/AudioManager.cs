using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider soundVolumeSlider = default;
    [SerializeField] private Slider musicVolumeSlider = default;

    private float soundVolume;
    private float musicVolume;

    public AudioClip[] soundEffects;
    private Dictionary<string, AudioClip> sounds;

    public AudioClip[] musicClips;
    private Dictionary<string, AudioClip> music;

    private AudioSource soundAudio;
    private AudioSource musicAudio;
    

    enum VolumePrefKeys
    {
        SOUND, MUSIC
    }

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if(audioSources.Length != 2)
        {
            throw new MissingComponentException("2 Audio sources needed in audio manager");
        }
        soundAudio = audioSources[0];
        musicAudio = audioSources[1];

        musicVolume = PlayerPrefs.GetFloat(VolumePrefKeys.MUSIC.ToString(), 0.8f);
        musicVolumeSlider.value = musicVolume;
        musicAudio.volume = musicVolume;
        musicAudio.loop = true;
        music = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in musicClips)
        {
            music.Add(clip.name.ToUpper(), clip);
        }

        soundVolume = PlayerPrefs.GetFloat(VolumePrefKeys.SOUND.ToString(), 0.8f);
        soundVolumeSlider.value = soundVolume;
        soundAudio.volume = soundVolume;
        soundAudio.loop = false;
        foreach (AudioClip clip in soundEffects)
        {
            sounds.Add(clip.name.ToUpper(), clip);
        }
    }

    public void PlaySound(string soundName)
    {
        string normalizedName = soundName.ToUpper();
        if(sounds.ContainsKey(normalizedName))
        {
            soundAudio.clip = sounds[normalizedName];
            soundAudio.Play();
        }
    }

    public void PlayMusic(string musicName)
    {
        string normalizedName = musicName.ToUpper();
        if (music.ContainsKey(normalizedName))
        {
            musicAudio.clip = music[normalizedName];
            musicAudio.Play();
        }
    }

    public void UpdateSoundVolume()
    {
        soundVolume = soundVolumeSlider.value;
        PlayerPrefs.SetFloat(VolumePrefKeys.SOUND.ToString(), soundVolume);
    }

    public void UpdateMusicVolume()
    {
        musicVolume = musicVolumeSlider.value;
        PlayerPrefs.SetFloat(VolumePrefKeys.MUSIC.ToString(), musicVolume);
    }

}
