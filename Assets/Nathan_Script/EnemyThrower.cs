using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyThrower : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public GameObject fireBallPrefab;
    public List<GameObject> exsitsThingToThrow = new List<GameObject>();
    public Transform throwPoint;
    public float minForce = 5f;
    public float maxForce = 8f;
    public float throwInterval = 2f;
    public float cooldown;
    public float cooldownTimer;

    public bool isStunning;
    public float stunningTime;
    private bool canStartCoroutine;
    

    void Start()
    {
        canStartCoroutine = true;
    }

    private IEnumerator isStunningTimeHAHA()
    {
        canStartCoroutine = false;
        yield return new WaitForSeconds(stunningTime);
        isStunning = false;
        
        canStartCoroutine = true;

    }

    private void Update()
    {
        if (isStunning == true)
        {
            if(canStartCoroutine == true)
                StartCoroutine(isStunningTimeHAHA());
            
            cooldownTimer = 0f;
            return;
        }
        
        if (cooldownTimer < cooldown)
        {
            cooldownTimer += Time.deltaTime;
            return;
        }

        if (isStunning == false) 
        {
            ThrowNTimesGO(fireBallPrefab,1);
        }
        
        
        

        cooldownTimer = 0f;
    }
    

    private void ThrowNTimesGO(GameObject thrownGO ,int howManyTimes)
    {
        
        AudioManager.Instance.PlaySfx("Dino_When dino spray shoot fireball");
        
        for (int i = 1; i <= howManyTimes; ++i)
        {
            
            ThrowCollectible(thrownGO);
        }
    }
    
    private void ThrowNTimesGO(GameObject[] thrownGO ,int howManyTimes)
    {
        for (int i = 1; i <= howManyTimes; ++i)
        {
            int randomArrayIndex = Random.Range(0, thrownGO.Length);
            
            ThrowCollectible(thrownGO[randomArrayIndex]);
        }
    }

    void ThrowCollectible(GameObject prefab)
    {
        GameObject thingToThrow = Instantiate(prefab, throwPoint.position, Quaternion.identity);
        exsitsThingToThrow.Add(thingToThrow);
        Rigidbody2D rb = thingToThrow.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            float forceX = Random.Range(7f, 12f); // Horizontal push
            float forceY = Random.Range(8f, 12f); // Upward lift

            // Throw to the left (negative X)
            Vector2 throwForce = new Vector2(-forceX, forceY);
            rb.AddForce(throwForce, ForceMode2D.Impulse);
 
        }
    }

    private void OnCollisionEnter2D(Collision2D fireBallCollider)
    {
        if (fireBallCollider.collider.tag != "LightEnemy")
            return;
        
        Debug.Log("Enemy is stunning");
        AudioManager.Instance.PlaySfx("Dino_When dino get stun");
        isStunning = true;
        ThrowNTimesGO(collectiblePrefabs,3);
        Destroy(fireBallCollider.gameObject);
        
        
        
            
    }
}



