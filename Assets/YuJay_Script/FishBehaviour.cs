using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public Transform patrolLeft;

    public Transform patrolRight;

    public float fishSpeed = 1;

     public bool beingTouched = false;

    public bool goLeft = true;

    public string isWhichItem_Name;

    public bool isMermaid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beingTouched == true)
            return;

        if (isMermaid == true)
            return;
        
        FlipFish();

        if (goLeft == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolLeft.position.x, transform.position.y), fishSpeed * Time.deltaTime);
        }
        else if (goLeft == false) 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolRight.position.x, transform.position.y), fishSpeed * Time.deltaTime);
        }

        CheckDistance();

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        beingTouched = true;
        
        

        if (isMermaid == true)
        {
            SingletonCraftingCanvas.theStaticCraftingCanvas.GetComponentInChildren<PointerBehaviour>().CanContinueToTrue();
            AudioManager.Instance.PlaySfx("Fishing_When catch the mermaid");
        }
        
        if (isWhichItem_Name != null && isWhichItem_Name!= "" && isMermaid == false) 
        {
            AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc(isWhichItem_Name);
            AudioManager.Instance.PlaySfx("Fishing_When catching fish");
            
        }
        
    }

    void CheckDistance()
    {
        if(goLeft == true)
        {
            if(Mathf.Abs(transform.position.x - patrolLeft.position.x) < 0.1f)
            {
                goLeft = false;
            }

        }
        else
        {
            if (Mathf.Abs(transform.position.x - patrolRight.position.x) < 0.1f)
            {
                goLeft = true;
            }
        }

        
    }

    void FlipFish()
    {
        if (goLeft == true)
        {

            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
