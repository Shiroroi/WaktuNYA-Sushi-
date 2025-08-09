using UnityEngine;

public class CyberStroyButtonEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LetContinue()
    {
        AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc("realMouse");
        AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc("pcMouse");
        AudioManager.Instance.PlaySfx("Menu_Button click sound");
        SingletonCraftingCanvas.theStaticCraftingCanvas.GetComponentInChildren<PointerBehaviour>().CanContinueToTrue();
        
    }
}
