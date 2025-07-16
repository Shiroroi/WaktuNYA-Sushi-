using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool canBeCollected = false;
    private bool playerInRange = false;
    private SpriteRenderer sr;

    public string itemName;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && !canBeCollected)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            StartCoroutine(BlinkAndTimeout());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (canBeCollected && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc(itemName);
                
                scoreManager.AddScore(1);
            }

            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator BlinkAndTimeout()
    {
        canBeCollected = true;

        float blinkTime = 3f;
        float blinkRate = 0.2f;
        float elapsed = 0f;

        while (elapsed < blinkTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkRate);
            elapsed += blinkRate;
        }

        Destroy(gameObject);
    }
}
