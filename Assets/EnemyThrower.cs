using UnityEngine;

public class EnemyThrower : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public Transform throwPoint;
    public float minForce = 5f;
    public float maxForce = 8f;
    public float throwInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(ThrowCollectible), 1f, throwInterval);
    }

    void ThrowCollectible()
    {
        GameObject collectible = Instantiate(collectiblePrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = collectible.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float forceX = Random.Range(7f, 12f); // Horizontal push
            float forceY = Random.Range(8f, 12f); // Upward lift

            // Throw to the left (negative X)
            Vector2 throwForce = new Vector2(-forceX, forceY);
            rb.AddForce(throwForce, ForceMode2D.Impulse);
        }
    }
}



