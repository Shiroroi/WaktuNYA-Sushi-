using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItem = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public int selectedSlot = -1;

    public static InventoryManager instance;
    

    public Item[] itemsToPickup;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }

        
        
        

    }

    void Start()
    {
        
        AddItem("rice");
        AddItem("rice");
        AddItem("rice");
        AddItem("rice");
        AddItem("rice");
        AddItem("rice");
        
        AddItem("tBoneSteak");
        AddItem("tBoneSteak");
        AddItem("tBoneSteak");
        
        AddItem("tamagoyaki");
        AddItem("tamagoyaki");
        AddItem("tamagoyaki");
        
        AddItem("soySauce");
        AddItem("soySauce");
        AddItem("jelloWasabi");
        AddItem("jelloWasabi");
        
        
        
    }


    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number <= 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }

        

    }


    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
            inventorySlots[selectedSlot].Deselect();

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public void AddItem(string itemName)
    {
        Item item = itemsToPickup.FirstOrDefault(item => item.name == itemName);

        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; ++i)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item.name == item.name && itemInSlot.count < maxStackedItem)
            {
                ++itemInSlot.count;
                itemInSlot.RefreshCount();
                return;
            }
        }

        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; ++i)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }

        return;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);

        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public void UseSelectedItem(InventorySlot slot)
    {
        
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            -- itemInSlot.count;
            
            if (itemInSlot.count <= 0)
            {
                Destroy(itemInSlot.gameObject);
            }
            else
            {
                itemInSlot.RefreshCount();
            }
            
            return;
        }
        
    }
    
    public void UseItemByName(string itemName)
    {
        // foreach inventory slot
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // cgeck the slot got item and item.name is what we want
            if (itemInSlot != null && itemInSlot.item.name == itemName)
            {
                // 找到了！调用现有的消耗逻辑
                UseSelectedItem(slot); 
                
                Debug.Log("Use selected item with name");
                // use one is enough
                return; 
            }
        }
        
        // if the loop end still not find
        Debug.LogWarning("bag no have " + itemName );
    }

    
}
