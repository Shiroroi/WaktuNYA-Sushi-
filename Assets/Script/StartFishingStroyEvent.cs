using UnityEngine;

public class StartFishingStroyEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryManager.instance.UseItemByName("bracelet");
    }

    
}
