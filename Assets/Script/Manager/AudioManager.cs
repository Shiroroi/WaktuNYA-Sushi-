using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // ... [所有字段，包括Sliders, sfx/music sounds, sources都保持不变] ...
    [Header("UI Sliders")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [Header("Music Settings")]
    [Tooltip("背景音乐淡入/淡出的时间")]
    [SerializeField] float musicFadeDuration = 2.0f;

    [Header("SFX Settings")]
    [Tooltip("音效的最小音高")]
    [SerializeField] float sfxMinPitch = 0.9f;
    [Tooltip("音效的最大音高")]
    [SerializeField] float sfxMaxPitch = 1.1f;
    
    [Header("Audio Sources & Clips")]
    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public static AudioManager Instance;
    private Coroutine activeMusicCoroutine; // Coroutine现在管理整个切换流程
    
    // --- Awake, Start, 音量控制, Load, UpdateVolume 函数都保持原样，无需修改 ---
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
        if (!PlayerPrefs.HasKey("masterVolume")) PlayerPrefs.SetFloat("masterVolume", 1);
        if (!PlayerPrefs.HasKey("musicVolume")) PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("sfxVolume")) PlayerPrefs.SetFloat("sfxVolume", 1);
        PlayerPrefs.Save();
        Load();
        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(ChangeMasterVolume);
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    public void ChangeMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
        UpdateSfxVolume(); 
        if (activeMusicCoroutine == null) UpdateMusicVolume();
    }

    public void ChangeMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        if (activeMusicCoroutine == null) UpdateMusicVolume();
    }

    public void ChangeSfxVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value);
        UpdateSfxVolume();
    }
    
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        UpdateMusicVolume();
        UpdateSfxVolume();
    }
    
    private void UpdateMusicVolume()
    {
        float masterVol = volumeSlider != null ? volumeSlider.value : 1f;
        float musicVol = musicSlider != null ? musicSlider.value : 1f;
        musicSource.volume = masterVol * musicVol;
    }
    
    private void UpdateSfxVolume()
    {
        float masterVol = volumeSlider != null ? volumeSlider.value : 1f;
        float sfxVol = sfxSlider != null ? sfxSlider.value : 1f;
        sfxSource.volume = masterVol * sfxVol;
    }


    
    public void PlayMusic(string _name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == _name);
        if (s == null)
        {
            Debug.LogWarning($"名为 '{_name}' 的音乐未找到");
            return;
        }

        // 如果要播放的已经是当前音乐，则不执行任何操作
        if (musicSource.clip == s.clip && musicSource.isPlaying)
        {
            return; 
        }

        // 停止任何可能正在运行的旧的切换流程，然后开始新的流程
        if (activeMusicCoroutine != null)
        {
            StopCoroutine(activeMusicCoroutine);
        }
        
        activeMusicCoroutine = StartCoroutine(FadeSwitchMusic(s.clip, musicFadeDuration));
    }

  
    private IEnumerator FadeSwitchMusic(AudioClip newClip, float duration)
    {
        // --- 1. 淡出阶段 ---
        if (musicSource.isPlaying) // 仅当有音乐正在播放时才执行淡出
        {
            float startVolume = musicSource.volume;
            float timer = 0f;

            while (timer < duration)
            {
                // 从当前音量平滑过渡到0
                musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        // --- 2. 切换阶段 ---
        musicSource.Stop(); // 停止旧音乐
        musicSource.volume = 0f;
        musicSource.clip = newClip; // 设置新音乐
        musicSource.Play(); // 播放新音乐（此时音量为0）

        // --- 3. 淡入阶段 ---
        if (duration > 0)
        {
            float fadeInTimer = 0f;
            while (fadeInTimer < duration)
            {
                // 在每一帧重新计算目标音量，以响应实时滑块调整
                float masterVol = volumeSlider != null ? volumeSlider.value : 1f;
                float musicVol = musicSlider != null ? musicSlider.value : 1f;
                float targetVolume = masterVol * musicVol;

                // 从0平滑过渡到目标音量
                musicSource.volume = Mathf.Lerp(0f, targetVolume, fadeInTimer / duration);

                fadeInTimer += Time.deltaTime;
                yield return null;
            }
        }
        
        // --- 4. 收尾阶段 ---
        UpdateMusicVolume(); // 确保最终音量完全准确
        activeMusicCoroutine = null; // 标志协程已结束
    }

    // --- SFX 播放函数保持不变 ---
    public void PlaySfx(bool randomPitch, params string[] _names)
    {
        // ... (省略未修改代码)
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
    public void PlaySfx(params string[] _names) => PlaySfx(true, _names);
}