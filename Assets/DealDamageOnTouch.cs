using System;
using UnityEngine;

public class DealDamageOnTouch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Health>() != null)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(1);
            }
        }
    }
}
