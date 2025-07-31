using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; 
    [SerializeField] Slider musicSlider;  
    [SerializeField] Slider sfxSlider;

    
    public float sfxMaxPitch = 0.9f;
    public float sfxMinPitch = 1.1f;
    
    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public static AudioManager Instance;

    private void Awake()
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
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }

        PlayerPrefs.Save();

        Load();

        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(ChangeMasterVolume);

        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    public void ChangeMasterVolume(float value)
    {
        float musicVolume = musicSlider != null ? musicSlider.value : 1f;
        float sfxVolume = sfxSlider != null ? sfxSlider.value : 1f;

        musicSource.volume = value * musicVolume;
        sfxSource.volume = value * sfxVolume;

        PlayerPrefs.SetFloat("masterVolume", value);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float value)
    {
        float masterVolume = volumeSlider != null ? volumeSlider.value : 1f;
        musicSource.volume = masterVolume * value;
        PlayerPrefs.SetFloat("musicVolume", value);
        PlayerPrefs.Save();
    }

    public void ChangeSfxVolume(float value)
    {
        float masterVolume = volumeSlider != null ? volumeSlider.value : 1f;
        sfxSource.volume = masterVolume * value;
        PlayerPrefs.SetFloat("sfxVolume", value);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        float masterVol = PlayerPrefs.GetFloat("masterVolume");
        float musicVol = PlayerPrefs.GetFloat("musicVolume");
        float sfxVol = PlayerPrefs.GetFloat("sfxVolume");

        if (volumeSlider != null)
            volumeSlider.value = masterVol;
        if (musicSlider != null)
            musicSlider.value = musicVol;
        if (sfxSlider != null)
            sfxSlider.value = sfxVol;

        AudioListener.volume = masterVol;
        musicSource.volume = musicVol;
        sfxSource.volume = sfxVol;
    }

    public void PlayMusic(string _name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == _name);

        if (s == null)
        {
            Debug.Log($"Music {_name} not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(bool randomPitch, params string[] _names)
    {
        if (_names == null || _names.Length == 0)
        {
            Debug.LogWarning("PlaySfx called with empty names");
            return;
        }
        
        string nameToPlay = _names[UnityEngine.Random.Range(0, _names.Length)];
        
        Sound s = Array.Find(sfxSounds, x => x.name == nameToPlay);

        if (s == null)
        {
            Debug.Log($"Sfx {nameToPlay} not found");
            return;
        }
        
        if(randomPitch == true)
        {
            float o_pitch = sfxSource.pitch;
            sfxSource.pitch = UnityEngine.Random.Range(sfxMinPitch, sfxMaxPitch);
            sfxSource.PlayOneShot(s.clip);
            sfxSource.pitch = o_pitch;
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    
    // if no say true or false, equal true
    public void PlaySfx(params string[] _names)
    {
        
        PlaySfx(true, _names);
    }
}
