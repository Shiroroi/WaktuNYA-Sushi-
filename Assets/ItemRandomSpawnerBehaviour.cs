using UnityEngine;

public class ItemRandomSpawnerBehaviour : MonoBehaviour
{
    
    public GameObject[] collectableToSpawn;
    private GameObject theCollectbleSpawn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomArrayIndex = Random.Range(0, collectableToSpawn.Length);
        
        theCollectbleSpawn = Instantiate(collectableToSpawn[randomArrayIndex], transform.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        Destroy(theCollectbleSpawn);
    }
    
   
}
