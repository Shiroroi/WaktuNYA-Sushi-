using UnityEngine;

public class NotiCanvasSigleton : MonoBehaviour
{
    public static GameObject notiCanvas;
    
    void Awake()
    {
        if (notiCanvas == null)
        {
            notiCanvas = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
