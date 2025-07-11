using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);

        //if(result == true)
        //{
        //    Debug.Log("Item added");
        //}
        //else
        //{
        //    Debug.Log("ITEM NOT ADDED");
        //}
    }



    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);

        //if (receivedItem != null)
        //    Debug.Log("Use item: " + receivedItem);
        //else
        //    Debug.Log("NO ITEM USED !!!");
    }
}
