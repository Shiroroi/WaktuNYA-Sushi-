using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    [Header("Scenes")]
    public Image slideImage;              // assign SlideImage here
    public Button nextButton;             // assign NextButton here
    public string[] slideNames;           // list of slide sprite names
    public string sceneToLoad = "MainMenu";
    public float slideDuration = 3f;      // how long each slide stays visible before auto-transition
    public float fadeDuration = 1f;       // how long fade in/out lasts

    private int currentSlideIndex = 0;
    private bool isTransitioning = false;

    public void OnNewGameClicked()
    {
        if (slideNames.Length > 0)
        {
            StartSlideshow();
            nextButton.onClick.AddListener(SkipToNextSlide);
        }

        PlayerPrefs.DeleteAll();
        Time.timeScale = 1f;

        foreach (var obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.scene.name == "DontDestroyOnLoad")
                Destroy(obj);
        }

        SingletonUICanvas.theStaticCanvas = null;
        SingletonCharacterCanvas.theStaticChracterCanvas = null;
    }

    void StartSlideshow()
    {
        Time.timeScale = 0f;
        slideImage.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        StartCoroutine(SlideshowCoroutine());
    }

    IEnumerator SlideshowCoroutine()
    {
        while (currentSlideIndex < slideNames.Length)
        {
            Sprite slide = Resources.Load<Sprite>("IntroScene/" + slideNames[currentSlideIndex]);
            if (slide == null)
            {
                Debug.LogWarning("Slide not found: " + slideNames[currentSlideIndex]);
                currentSlideIndex++;
                continue;
            }

            slideImage.sprite = slide;
            yield return StartCoroutine(FadeImage(0f, 1f)); // fade in

            float elapsed = 0f;
            isTransitioning = false;

            // Wait for duration or button press
            while (elapsed < slideDuration && !isTransitioning)
            {
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            yield return StartCoroutine(FadeImage(1f, 0f)); // fade out
            currentSlideIndex++;
        }

        EndSlideshow();
    }

    void SkipToNextSlide()
    {
        if (!isTransitioning)
            isTransitioning = true;
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = slideImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            slideImage.color = color;
            yield return null;
        }
    }

    void EndSlideshow()
    {
        Time.timeScale = 1f;
        Debug.Log("Slideshow ended. Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked! Exiting application...");
        Application.Quit();
    }
}
