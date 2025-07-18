using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class PointerBehaviour : MonoBehaviour
{
    public RectTransform pointA;
    public RectTransform pointB;
    public Collider2D goodAreaCollider;

    public Vector2 leftBound;
    public Vector2 rightBound;

    [HideInInspector]public Vector2 target;

    public float moveSpeed;

    [HideInInspector]public bool nowCheck;
    public Vector2 myCheckSize;
    [HideInInspector] public bool suces;

    public bool canCraft;

    public InventorySlot CraftBlock1;
    public InventorySlot CraftBlock2;
    public InventorySlot CraftBlock3;

    public Sushi[] sushi;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftBound = pointA.position;
        rightBound = pointB.position;
        
        target = leftBound;
        
        
        canCraft = false;
    }

    void OnEnable()
    {
        transform.position = pointB.position;
    }
        
    // Update is called once per frame
    void Update()
    {
        PingPongAndCheck();
    }

    private void PingPongAndCheck()
    {
        Collider2D[] checkToWhatCollier2D = Physics2D.OverlapBoxAll(transform.position, myCheckSize, 0f);
        suces = checkToWhatCollier2D.Contains(goodAreaCollider);
        
        
        if (nowCheck == true && suces == false)
        {
            canCraft = false;
            nowCheck = false;
            
        }
        else if (nowCheck == true && suces == true)  
        {
            canCraft = true;
            
            return;
        }
        else
        {
            canCraft = false;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if(target == leftBound)
                target = rightBound;
            else if(target == rightBound)
                target = leftBound;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, myCheckSize);

    }

    public void CheckFunction()
    {
        if (nowCheck == false)
        {
            InventoryManager.instance.UseSelectedItem(CraftBlock1);
            InventoryManager.instance.UseSelectedItem(CraftBlock2);
            InventoryManager.instance.UseSelectedItem(CraftBlock3);

            if (CraftBlock3.GetComponentInChildren<InventoryItem>()?.item.name == "rice")
            {
                string material1 = CraftBlock1.GetComponentInChildren<InventoryItem>()?.item.name;
                string material2 = CraftBlock2.GetComponentInChildren<InventoryItem>()?.item.name;

                bool foundMatch = false;
                
                foreach (Sushi s in sushi)
                {
                    if (s.m1 == material1 && s.m2 == material2)
                    {
                        Debug.Log(s.name + " is created");
                        foundMatch = true;
                        break;
                    }
                }
                
                if(foundMatch == false)
                    Debug.Log("No this recipe");
                
            }
                



        }
        
        
        nowCheck = !nowCheck;
    }
}
    