using System;
using UnityEngine;

public class SonOfFBBehaviour : MonoBehaviour
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
        if (other.gameObject.layer == LayerMask.NameToLayer("SonOfFireball"))
            return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            return;

        if (other.gameObject.tag == "Vertical wall")
            return;
        
        AudioManager.Instance.PlaySfx("Dino_When small fireball touch the ground");
        Destroy(gameObject);
    }   
}
