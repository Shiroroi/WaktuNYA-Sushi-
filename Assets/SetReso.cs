using UnityEngine;

public class SetReso : MonoBehaviour
{
    public ResolutionChanger  resolutionChanger;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        resolutionChanger.ChangeResolutionTo1K();
        resolutionChanger.SetFPS60();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
