using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#pragma warning disable CS0162 // Unreachable code detected

public class FireballBehaviour : MonoBehaviour
{
    public bool shouldBurnOut;

    public bool canEndDetection;

    public int numberOfSon_smallThenRaycount;

    public GameObject SonOfFireball;
    public float sonForce;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shouldBurnOut = true;
        canEndDetection = false;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {

        if (collision2D.gameObject.tag == "Thrower")
            return;

        if (shouldBurnOut == true && canEndDetection == false)
        {
            
            
            for (int i = 1; i <= numberOfSon_smallThenRaycount; ++i)
            {
                float randomOfset = Random.Range(75f, 105f);
                float finalAngle = (randomOfset) * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle)).normalized;

                
                GameObject son = Instantiate(SonOfFireball, transform.position, Quaternion.identity);
                son.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) * sonForce, ForceMode2D.Impulse);



            }
            
            
            
            GetComponent<Health>().TakeDamage(1000);
            
            canEndDetection = true;
        }


    }
    

    
}
