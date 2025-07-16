using UnityEngine;

public class SingletonUICanvas : MonoBehaviour
{
 
    public static GameObject theStaticCanvas;
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
