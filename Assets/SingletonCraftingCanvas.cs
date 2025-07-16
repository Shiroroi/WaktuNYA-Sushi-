using UnityEngine;

public class SingletonCraftingCanvas : MonoBehaviour
{
    public static GameObject theStaticCraftingCanvas;
    void Awake()
    {
        if(theStaticCraftingCanvas == null)
        {
            theStaticCraftingCanvas = this.gameObject;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
