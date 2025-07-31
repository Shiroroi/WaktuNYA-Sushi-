using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] Slider volumeSlider; 
    [SerializeField] Slider musicSlider;  
    [SerializeField] Slider sfxSlider;

    [Header("Music Settings")]
    [Tooltip("背景音乐淡入所需时间")]
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
    private Coroutine activeMusicFadeCoroutine;
    
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
        // 检查并设置默认 PlayerPrefs
        if (!PlayerPrefs.HasKey("masterVolume")) PlayerPrefs.SetFloat("masterVolume", 1);
        if (!PlayerPrefs.HasKey("musicVolume")) PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("sfxVolume")) PlayerPrefs.SetFloat("sfxVolume", 1);
        PlayerPrefs.Save();

        // 加载音量并设置监听器
        Load();

        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(ChangeMasterVolume);
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    // --- 经过优化的音量控制函数 ---

    public void ChangeMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
        UpdateSfxVolume(); // 实时更新音效音量
        
        // 只有在没有BGM淡入时，才直接更新音乐音量，以防冲突
        if (activeMusicFadeCoroutine == null)
        {
            UpdateMusicVolume();
        }
    }

    public void ChangeMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        if (activeMusicFadeCoroutine == null)
        {
            UpdateMusicVolume();
        }
    }

    public void ChangeSfxVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value);
        UpdateSfxVolume();
    }
    
    // --- 经过优化的加载与更新函数 ---
    
    private void Load()
    {
        // 从PlayerPrefs加载值并设置滑块
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        
        // 使用更新函数来确保初始音量是正确的
        UpdateMusicVolume();
        UpdateSfxVolume();
    }
    
    // 创建了统一的更新函数，以避免代码重复并确保逻辑一致
    private void UpdateMusicVolume()
    {
        // 使用滑块的实时值进行计算，保证了响应性
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

    // --- BGM 播放与智能淡入 ---

    public void PlayMusic(string _name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == _name);
        if (s == null)
        {
            Debug.LogWarning($"名为 '{_name}' 的音乐未找到");
            return;
        }
        
        // 停止任何正在进行的旧淡入协程
        if(activeMusicFadeCoroutine != null) StopCoroutine(activeMusicFadeCoroutine);
        
        activeMusicFadeCoroutine = StartCoroutine(FadeInMusic(s.clip, musicFadeDuration));
    }

    // 这是一个更智能的协程，它可以响应实时的音量变化
    private IEnumerator FadeInMusic(AudioClip clip, float duration)
    {
        musicSource.clip = clip;
        musicSource.Play();
        musicSource.volume = 0f;
        
        if (duration <= 0)
        {
            UpdateMusicVolume(); // 直接设置为当前正确的目标音量
            activeMusicFadeCoroutine = null;
            yield break;
        }
        
        float timer = 0f;
        while (timer < duration)
        {
            // 关键优化：在每一帧都重新计算目标音量！
            // 这使得淡入效果可以动态适应用户在淡入期间对滑块的调整。
            float masterVol = volumeSlider != null ? volumeSlider.value : 1f;
            float musicVol = musicSlider != null ? musicSlider.value : 1f;
            float targetVolume = masterVol * musicVol;

            // 平滑过渡到新的目标音量
            musicSource.volume = Mathf.Lerp(0f, targetVolume, timer / duration);
            
            timer += Time.deltaTime;
            yield return null; 
        }
        
        // 确保最终音量完全准确
        UpdateMusicVolume(); 
        activeMusicFadeCoroutine = null; // 协程结束，清空引用
    }

    // --- SFX 播放（保持不变） ---

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
            // 修正：确保Min比Max小
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