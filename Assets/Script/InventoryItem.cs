using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Item item;
    public Text countText;
    public GameObject itemName;


    [Header("UI")]
    [HideInInspector] public Image image;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();

        string name;

        if (newItem.displayName == null || newItem.displayName == "")
        {
            name = newItem.name;
        }
        else
        {
            name  = newItem.displayName;
        }
        
        
        if (!string.IsNullOrEmpty(name))
        {
            name = char.ToUpper(name[0]) + name.Substring(1);
        }
        
        itemName.GetComponent<TMP_Text>().text = name;
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // set parent to canvas

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemName.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemName.SetActive(false);
    }
}
