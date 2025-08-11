using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField]
    private Material screenTransitionMaterial;

    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private string propertyName = "_Progress";

    private float delayTime = 1f;
    public UnityEvent OnTransitionDone;
    
    

    private void Start()
    {
        
        StartCoroutine(TransitionCoroutine());
    }
    
    
    private IEnumerator TransitionCoroutine()
    {
        screenTransitionMaterial.SetFloat(propertyName, 0f);
        yield return new WaitForSeconds(delayTime);
        
        float currentTime = 0f;
        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            screenTransitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
            yield return null;
        }
        OnTransitionDone?.Invoke();
    }
    
}
