using Unity.Cinemachine;
using UnityEngine;

public class SingletonUICanvas : MonoBehaviour
{
    public static GameObject theStaticCanvas;
    
    public Transform mainCameraTransform;
    
   
    void Awake()
    {
        if(theStaticCanvas == null)
        {
            theStaticCanvas = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    



}
