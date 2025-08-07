using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

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
        
        gameObject.GetComponent<RectTransform>().pivot = new  Vector2(0f, 1.3f);
    }

    public void AddToSmallInventoryAndBigFunc(string itemName)
    {
        
        for(int i = 0; i < images.Length; ++i)
        {
            
            Image currentImage = images[i];
            Image imageInSlot = currentImage.GetComponentsInChildren<Image>(includeInactive: true).FirstOrDefault(img => img.gameObject != currentImage.gameObject);
            
            if (imageInSlot == null)
            {
                // instansiate a image
                GameObject newImage = Instantiate(Samll_Inven_InSlot_Image, currentImage.transform);
                
                // set dia punya image equal to the one of the item in the list punya image property
                newImage.GetComponent<Image>().sprite = itemsToPickUp.FirstOrDefault(item => item.name == itemName)?.image;

                if (String.IsNullOrEmpty(itemsToPickUp.FirstOrDefault(item => item.name == itemName)?.displayName) ==
                    false)
                {
                    newImage.GetComponentInChildren<TMP_Text>().text = itemsToPickUp.FirstOrDefault(item => item.name == itemName)?.displayName;    
                }
                else
                {
                    newImage.GetComponentInChildren<TMP_Text>().text = itemName;
                }
                
                // add to main inventory with same name that pass in to this function
                InventoryManager.instance.AddItem(itemName);
                
                return;
            }
        }
    }
}
