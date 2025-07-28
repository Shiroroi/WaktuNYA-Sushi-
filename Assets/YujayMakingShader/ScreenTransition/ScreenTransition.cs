using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField]
    private Material screenTransitionMaterial;

    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private string propertyName = "_Progress";
    
    public UnityEvent OnTransitionDone;

    private void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
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
