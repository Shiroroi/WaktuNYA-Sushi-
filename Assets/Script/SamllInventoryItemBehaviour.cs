using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class SamllInventoryItemBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public TMP_Text itemName;
    public Color showColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemName.color = showColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemName.color = new Color(0, 0, 0, 0);
    }
}
