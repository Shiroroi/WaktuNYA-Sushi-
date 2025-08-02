using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthGroupBehaviour : MonoBehaviour
{
    
    public Health playerHealth;
    public GameObject heartPrefab;
    public Sprite heartDamageImage;
    
    public List<GameObject> finalHeartList;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < playerHealth.maxHealth; ++i)
        {
            finalHeartList.Add(GameObject.Instantiate(heartPrefab, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHeart()
    {
        for (int i = 0; i < (finalHeartList.Count - playerHealth.currentHealth); ++i)
        {
            try
            {
                Debug.Log(i);
                finalHeartList[i].GetComponent<Image>().sprite = heartDamageImage;
            }
            catch
            {
                Debug.Log("BreakOut");
                break;
            }
            
        }
    }
    
    
}
