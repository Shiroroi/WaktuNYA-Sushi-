using UnityEngine;

public class BgmSoundActivater : MonoBehaviour
{

    public string bgmName;
    public float tempAudioFadeDuration; // only within this level
    public bool useTempAudioFadeDuration;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AudioManager.Instance == null)
        {
            // prepare for start menu 
            return;
        }
        
        if (bgmName != null)
        {
            AudioManager.Instance.PlayMusic(bgmName);

            if (useTempAudioFadeDuration == true)
            {
                AudioManager.Instance.musicFadeDuration =  tempAudioFadeDuration;
            }
        }
    }
    
}
