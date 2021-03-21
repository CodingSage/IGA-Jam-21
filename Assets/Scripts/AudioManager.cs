using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
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

    public AudioClip walkingSound;
    public AudioClip pushingSound;

    private AudioSource soundAudio;
    private AudioSource musicAudio;
    private AudioSource walkingSoundAudio;
    private AudioSource pushingSoundAudio;

    public bool WalkingSoundPlaying
    {
        get { return walkingSoundAudio.isPlaying; }
    }

    public bool PushingSoundPlaying
    {
        get { return pushingSoundAudio.isPlaying; }
    }

    enum VolumePrefKeys
    {
        SOUND, MUSIC
    }

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if(audioSources.Length != 4)
        {
            throw new MissingComponentException("4 Audio sources needed in audio manager");
        }
        soundAudio = audioSources[0];
        musicAudio = audioSources[1];
        walkingSoundAudio = audioSources[2];
        pushingSoundAudio = audioSources[3];

        musicVolume = PlayerPrefs.GetFloat(VolumePrefKeys.MUSIC.ToString(), 0.8f);
        musicVolumeSlider.value = musicVolume;
        musicAudio.volume = musicVolume * 0.25f;
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
        sounds = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in soundEffects)
        {
            sounds.Add(clip.name.ToUpper(), clip);
        }

        if(musicClips.Length != 0)
        {
            PlayMusic(musicClips[0].name);
        }

        walkingSoundAudio.volume = soundVolume;
        walkingSoundAudio.loop = true;
        walkingSoundAudio.clip = walkingSound;

        pushingSoundAudio.volume = soundVolume;
        pushingSoundAudio.loop = true;
        pushingSoundAudio.clip = pushingSound;
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

    public void PlayWalkingSound()
    {
        walkingSoundAudio.Play();
    }

    public void StopWalkingSound()
    {
        if (walkingSoundAudio.isPlaying)
        {
            walkingSoundAudio.Stop();
        }
    }

    public void PlayPushingSound()
    {
        pushingSoundAudio.Play();
    }

    public void StopPushingSound()
    {
        if (pushingSoundAudio.isPlaying)
        {
            pushingSoundAudio.Stop();
        }
    }

    public void UpdateSoundVolume()
    {
        soundVolume = soundVolumeSlider.value;
        soundAudio.volume = soundVolume;
        walkingSoundAudio.volume = soundVolume;
        pushingSoundAudio.volume = soundVolume;
        PlayerPrefs.SetFloat(VolumePrefKeys.SOUND.ToString(), soundVolume);
    }

    public void UpdateMusicVolume()
    {
        musicVolume = musicVolumeSlider.value;
        musicAudio.volume = musicVolume * 0.25f;
        PlayerPrefs.SetFloat(VolumePrefKeys.MUSIC.ToString(), musicVolume);
    }

}
