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

    public Vector2 target;

    public float moveSpeed;

    public bool nowCheck;
    public Vector2 myCheckSize;
    public bool suces;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftBound = pointA.position;
        rightBound = pointB.position;
        
        target = leftBound;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowCheck == true)
        {

            Collider2D[] checkToWhatCollier2D = Physics2D.OverlapBoxAll(transform.position, myCheckSize, 0f);

            suces = checkToWhatCollier2D.Contains(goodAreaCollider);
            
            
            Debug.Log(suces);
            
        }
        else if(nowCheck==false)
            PingPong();
            
    }

    private void PingPong()
    {
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
}
    