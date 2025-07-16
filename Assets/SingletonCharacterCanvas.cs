using UnityEngine;

public class SingletonCharacterCanvas : MonoBehaviour
{
    public static GameObject theStaticChracterCanvas;
    void Awake()
    {
        if(theStaticChracterCanvas == null)
        {
            theStaticChracterCanvas = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
