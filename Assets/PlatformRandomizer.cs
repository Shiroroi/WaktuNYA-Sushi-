using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformRandomizer : MonoBehaviour
{

    public List<GameObject> platformLocationsSet;

    public float changePlatformSetTime;

    [SerializeField]
    public float timer = 0;

    private GameObject currentPlatformSet;
    
    public RopeController ropeController;
    
    
    void Start()
    {
        SpawnRandomPlatform();
    }

    void Update()
    {
        if (timer < changePlatformSetTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            ropeController.InitializePlayerAndTarget();
            ropeController.CloseGrappleRopeAndInitialize();
            Destroy(currentPlatformSet);
            SpawnRandomPlatform();
        }
    }
    
    void SpawnRandomPlatform()
    {
        int ramdomIndex = Random.Range(0, platformLocationsSet.Count);

        currentPlatformSet = Instantiate(platformLocationsSet[ramdomIndex]);
    }
    
    

    
}
