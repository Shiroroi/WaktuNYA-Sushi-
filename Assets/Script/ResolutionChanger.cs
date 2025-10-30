using UnityEngine;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeResolutionTo1K()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
    }
    
    public void ChangeResolutionTo2K()
    {
        Screen.SetResolution(2560, 1440, Screen.fullScreenMode);
    }

    public void ChangeFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    
    public void SetFPS30()
    {
        Application.targetFrameRate = 30;
    }

    public void SetFPS60()
    {
        Application.targetFrameRate = 60;
    } 
    public void SetFPS120()
    {
        Application.targetFrameRate = 120;
    }
    public void SetFPSNoLimit()
    {
        Application.targetFrameRate = -1;
    }
}
