using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
     // Change to your first scene name

    [Header("Scenes")]
    public Image slideImage; // assign SlideImage here
    public Button nextButton; // assign NextButton here
    public string[] slideNames;
    private int currentSlideIndex = 0;
    public string sceneToLoad = "MainMenu";

    public void OnNewGameClicked()
    {
        if (slideNames.Length > 0)
        {
            StartSlideshow();
            nextButton.onClick.AddListener(NextSlide);
        }

        
        // Clear saved data
        PlayerPrefs.DeleteAll();

        // Reset time in case it was frozen
        Time.timeScale = 1f;

        // Destroy any DontDestroyOnLoad managers
        foreach (var obj in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.scene.name == "DontDestroyOnLoad") // these objects persist across scenes
            {
                Destroy(obj);
            }
        }

        SingletonUICanvas.theStaticCanvas = null;
        SingletonCharacterCanvas.theStaticChracterCanvas = null;
        // Optionally reset static variables manually here
        // GameManager.ResetGame(); etc.

        // Load the first scene
        //SceneManager.LoadScene(startingScene);
    }
    void StartSlideshow()
    {
        Time.timeScale = 0f;
        slideImage.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        ShowSlide(currentSlideIndex);
    }
    void ShowSlide(int index)
    {
        Sprite slide = Resources.Load<Sprite>("IntroScene/" + slideNames[index]);
        if (slide != null)
        {
            slideImage.sprite = slide;
        }
        else
        {
            Debug.LogWarning("Slide not found: " + slideNames[index]);
        }
    }
    void NextSlide()
    {
        currentSlideIndex++;
        if (currentSlideIndex < slideNames.Length)
        {
            ShowSlide(currentSlideIndex);
        }
        else
        {
            EndSlideshow();
        }
    }

    void EndSlideshow()
    {
        Time.timeScale = 1f;
        Debug.Log("Slideshow ended. Loading scene: " + sceneToLoad);
        //SingletonUICanvas.theStaticCanvas.gameObject.SetActive(true);
        //SingletonCharacterCanvas.theStaticChracterCanvas.gameObject.SetActive(true);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void QuitGame()
    {
        Debug.Log("Quit button clicked! Exiting application...");

        // If running in a built application (Windows, Mac, Android, iOS, WebGL, etc.)
        Application.Quit();

        //        // If running in the Unity Editor
        //        // This line only works in the Editor and will not be included in a built game.
        //#if UNITY_EDITOR
        //        EditorApplication.isPlaying = false;
        //#endif
    }
}
