using UnityEngine;
using System.Collections;

public class UITransition : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    
    [SerializeField] private float transitionTime;
    
    [SerializeField] private Vector3 startPosition, endPosition;
    

    private void Awake()
    {
        startPosition = rectTransform.anchoredPosition;
    }

    public void ShowUI()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        float currentTime = 0f;
        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, Vector3.zero, Mathf.Clamp01(currentTime / transitionTime));
            yield return null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
