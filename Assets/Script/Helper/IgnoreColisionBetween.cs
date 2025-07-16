using UnityEngine;

public class IgnoreColisionBetween : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("SonOfFireball"), LayerMask.NameToLayer("Enemy"), true);
        // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("SonOfFireball"), LayerMask.NameToLayer("SonOfFireball"), true);
        
        
    }

}
