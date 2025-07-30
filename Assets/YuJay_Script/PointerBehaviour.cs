using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

using System.Collections;

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
    [HideInInspector] public bool contain;

    public bool canCraft;

    public InventorySlot CraftBlock1;
    public InventorySlot CraftBlock2;
    public InventorySlot CraftBlock3;

    public Sushi[] sushi;
    
    public Dialogue_menu[]  dialogue_menus;

    public bool haveStartCoroutine;

    
    
    
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
        contain = checkToWhatCollier2D.Contains(goodAreaCollider);
        
        
        if (nowCheck == true && contain == false)
        {
            nowCheck = false;
        }
        else if (nowCheck == true && contain == true && canCraft == true)
        {
            return;
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


    public void CheckCanCraft()
    {
        
        
        string material1 = CraftBlock1.GetComponentInChildren<InventoryItem>()?.item.name;
        string material2 = CraftBlock2.GetComponentInChildren<InventoryItem>()?.item.name;
        string material3 = CraftBlock3.GetComponentInChildren<InventoryItem>()?.item.name;
        
        if (material1 == "rice" || material2 == "rice" || material3 == "rice")
        {
            
            foreach (Sushi s in sushi)
            {
                if ((s.m1 == material1 || s.m1 == material2 || s.m1 == material3) &&
                    (s.m2 == material1 || s.m2 == material2 || s.m2 == material3))
                {
                    canCraft = true;
                    break;
                }
                else
                {
                    canCraft = false;
                }
 
            }
        }
        else
        {
            canCraft = false;
        }
        
    }
    
    public void CheckFunction()
    {
        // if(haveStartCoroutine == false)
        //     StartCoroutine(GoAutoOnceLater());
        
        CheckCanCraft();
        nowCheck = !nowCheck;
        
        if (contain == true && canCraft == true && nowCheck)
        {
            
            InventoryManager.instance.UseSelectedItem(CraftBlock1);
            InventoryManager.instance.UseSelectedItem(CraftBlock2);
            InventoryManager.instance.UseSelectedItem(CraftBlock3);
            
            Debug.Log("Sushi has been created");
            CanContinueToTrue();
        }
        
    }
    
    // public void CheckFunction2()
    // {
    //     
    //     
    //     CheckCanCraft();
    //     nowCheck = !nowCheck;
    //     
    //     if (contain == true && canCraft == true && nowCheck)
    //     {
    //         
    //         InventoryManager.instance.UseSelectedItem(CraftBlock1);
    //         InventoryManager.instance.UseSelectedItem(CraftBlock2);
    //         InventoryManager.instance.UseSelectedItem(CraftBlock3);
    //         
    //         Debug.Log("Sushi has been created");
    //         CanContinueToTrue();
    //     }
    //     
    // }
    //
    // public IEnumerator GoAutoOnceLater()
    // {
    //     haveStartCoroutine = true;
    //     yield return new WaitForSeconds(2f);
    //     CheckFunction2();
    //     haveStartCoroutine = false;
    // }
    
    public void CanContinueToTrue()
    {
        foreach (Dialogue_menu m in dialogue_menus)
        {
            m.canContinue = true;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, myCheckSize);

    }
}
    