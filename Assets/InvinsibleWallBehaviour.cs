using UnityEngine;

public class InvinsibleWallBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int myLayer = gameObject.layer;
        
        for (int i = 0; i < 32; ++i)
        {
            Physics2D.IgnoreLayerCollision(myLayer, i, true);
        }
        
        int playerLayer = LayerMask.NameToLayer("Player");
        
        Physics2D.IgnoreLayerCollision(myLayer, playerLayer, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
