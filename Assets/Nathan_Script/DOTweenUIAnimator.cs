using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Don't forget this!

public class DOTweenUIAnimator : MonoBehaviour
{
    public CanvasGroup uiPanelCanvasGroup;
    public float animationDuration = 0.3f;
    public Ease easeType = Ease.OutQuad; // Default easing (for regular fades/moves)

    void Awake()
    {
        if (uiPanelCanvasGroup == null)
        {
            uiPanelCanvasGroup = GetComponent<CanvasGroup>();
            if (uiPanelCanvasGroup == null)
            {
                uiPanelCanvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        uiPanelCanvasGroup.alpha = 0f;
        uiPanelCanvasGroup.interactable = false;
        uiPanelCanvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
        uiPanelCanvasGroup.alpha = 0f;
        uiPanelCanvasGroup.interactable = true;
        uiPanelCanvasGroup.blocksRaycasts = true;

        uiPanelCanvasGroup.DOFade(1f, animationDuration)
            .SetEase(easeType)
            .SetUpdate(UpdateType.Normal, true);
    }

    public void CloseUI()
    {
        uiPanelCanvasGroup.interactable = false;
        uiPanelCanvasGroup.blocksRaycasts = false;

        uiPanelCanvasGroup.DOFade(0f, animationDuration)
            .SetEase(easeType)
            .SetUpdate(UpdateType.Normal, true)
            .OnComplete(() => gameObject.SetActive(false));
    }

    // --- CORRECTED METHOD ---
    public void OpenUIAndPunchScale()
    {
        OpenUI(); // First, call the regular OpenUI to fade it in

        // Use DOPunchScale for the punch effect
        // parameters are: Vector3 punch, float duration, int vibrato, float elasticity
        uiPanelCanvasGroup.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f, 10, 1f) // Punch by 10% (0.1f), over 0.5s, with 10 vibrato, elasticity 1f
            .SetUpdate(UpdateType.Normal, true); // Keep it working during Time.timeScale = 0
    }
}
