using UnityEngine;

public class CyberStroyButtonEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LetContinue()
    {
        SingletonCraftingCanvas.theStaticCraftingCanvas.GetComponentInChildren<PointerBehaviour>().CanContinueToTrue();
    }
}
