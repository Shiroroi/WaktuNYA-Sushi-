using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AddToSmallInventory : MonoBehaviour
{

    public static AddToSmallInventory instance;

    public Image[] images;
    public GameObject Samll_Inven_InSlot_Image;
    public Item[] itemsToPickUp;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddToSmallInventoryAndBigFunc(string itemName)
    {
        for(int i = 0; i < images.Length; ++i)
        {
            
            Image currentImage = images[i];
            Image imageInSlot = currentImage.GetComponentsInChildren<Image>(includeInactive: true).FirstOrDefault(img => img.gameObject != currentImage.gameObject);



            if (imageInSlot == null)
            {
                
                GameObject newImage = Instantiate(Samll_Inven_InSlot_Image, currentImage.transform);     
                newImage.GetComponent<Image>().sprite = itemsToPickUp.FirstOrDefault(item => item.name == itemName)?.image;

                InventoryManager.instance.AddItem(itemName);
                return;
            }
        }
    }
}
