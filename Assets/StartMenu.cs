using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    [Header("Scenes")]
    public Image slideImage;              // assign SlideImage here
    public string[] slideNames;           // names of sprites in Resources/IntroScene/
    public string sceneToLoad = "MainMenu";
    public float slideDuration = 3f;      // how long each slide stays visible
    public float fadeDuration = 1f;       // how long fade in/out lasts

    private int currentSlideIndex = 0;

    public void OnNewGameClicked()
    {
        if (slideNames.Length > 0)
            StartCoroutine(SlideshowCoroutine());

        // Clear saved data
        PlayerPrefs.DeleteAll();

        // Reset time in case it was frozen
        Time.timeScale = 1f;

        // Destroy any DontDestroyOnLoad managers
        foreach (var obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.scene.name == "DontDestroyOnLoad")
                Destroy(obj);
        }

        SingletonUICanvas.theStaticCanvas = null;
        SingletonCharacterCanvas.theStaticChracterCanvas = null;
    }

    private IEnumerator SlideshowCoroutine()
    {
        Time.timeScale = 0f; // pause gameplay
        slideImage.gameObject.SetActive(true);

        for (currentSlideIndex = 0; currentSlideIndex < slideNames.Length; currentSlideIndex++)
        {
            Sprite slide = Resources.Load<Sprite>("IntroScene/" + slideNames[currentSlideIndex]);
            if (slide == null)
            {
                Debug.LogWarning("Slide not found: " + slideNames[currentSlideIndex]);
                continue;
            }

            slideImage.sprite = slide;
            yield return StartCoroutine(FadeImage(0f, 1f)); // fade in
            yield return new WaitForSecondsRealtime(slideDuration);
            yield return StartCoroutine(FadeImage(1f, 0f)); // fade out
        }

        EndSlideshow();
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha)
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

    private void EndSlideshow()
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

