using UnityEngine;

public class SingletonCraftingCanvas : MonoBehaviour
{
     public static GameObject theStaticCraftingCanvas;
    public void HelpCraftingCanvasSingelton()
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
