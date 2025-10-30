using UnityEngine;

public class CyberStoryAudioEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBrokenSound()
    {
        AudioManager.Instance.PlaySfx("Cyber_When fail to craft a material");
    }
}
