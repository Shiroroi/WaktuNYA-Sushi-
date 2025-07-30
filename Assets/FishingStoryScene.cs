using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FishingStoryScene : MonoBehaviour
{
    [Header("Camera Settings")]
    [Range(1, 10)] public float cameraSpeed = 5f;
    public Transform seaTop;
    public GameObject targetPositionUp;
    public GameObject targetPositiontDown;

    [Header("Movement Profiles")]
    public MovementHelper_original movementHelper;
    //public AnimationCurve goDownCurve;
    //public AnimationCurve forceGoUpCurve;
    //public AnimationCurve goUpCurve;

    [Header("Scenes")]
    public Image slideImage; // assign SlideImage here
    public Button nextButton; // assign NextButton here
    public string[] slideNames;
    private int currentSlideIndex = 0;
    public string sceneToLoad = "MainMenu";



    // private variable
    private Camera _camera;
    private CameraMotion _currentMotion = CameraMotion.stationary;
    public CameraMotion CurrentMotion // public properties, let other script the access our _currentMotion
    {
        get { return _currentMotion; }
    }
    private bool _canStartDescent = true;

    public enum CameraMotion
    {
        stationary,
        goDown,
        goUp
    }

    void Start()
    {
        _camera = Camera.main;

        // Defensive programming
        if (targetPositionUp == null || targetPositiontDown == null || movementHelper == null)
        {
            Debug.Log(gameObject.name + " missing something");
            this.enabled = false; // disable this script
        }
    }


    void Update()
    {

        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        // Only when camera motion is stationary, will check if
        if (_currentMotion == CameraMotion.stationary)
        {
            // the mouse pointer is under the sea top
            if (_camera.ScreenToWorldPoint(Input.mousePosition).y < seaTop.position.y && _canStartDescent)
            {
                //StartMovingDown();
                if (slideNames.Length > 0)
                {
                    StartSlideshow();
                }

                nextButton.onClick.AddListener(NextSlide);
            }
            // the mouse pointer is above the sea top
            else if (_camera.ScreenToWorldPoint(Input.mousePosition).y > seaTop.position.y)
            {
                _canStartDescent = true;
            }
        }
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
        Sprite slide = Resources.Load<Sprite>("FishStoryScene/" + slideNames[index]);
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
        SceneManager.LoadScene(sceneToLoad);
    }
}