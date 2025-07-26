using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public string tutorialPlayerPrefKey = "HasShownDinoGameTutorial"; // Example key for this mini-game
    public Button closeTutorialButton;


    void Start()
    {
        if (PlayerPrefs.GetInt(tutorialPlayerPrefKey, 0) == 0)
        {
            ShowTutorial();
        }
        else
        {
            HideTutorial(); // Ensure it's hidden and game is running if already shown
        }

        if (closeTutorialButton != null)
        {
            closeTutorialButton.onClick.AddListener(OnCloseTutorialButtonClicked);
        }
    }

    void ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true); // Make the panel visible

            // --- IMPORTANT: Pause the game ---
            Time.timeScale = 0f; // Setting timeScale to 0 pauses all time-dependent operations

            // Set the PlayerPref to indicate that this tutorial has now been shown
            PlayerPrefs.SetInt(tutorialPlayerPrefKey, 1);
            PlayerPrefs.Save(); // Save PlayerPrefs immediately (good practice)
        }
    }

    void HideTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false); // Make the panel invisible

            // --- IMPORTANT: Resume the game ---
            Time.timeScale = 1f; // Setting timeScale back to 1 resumes the game
        }
    }

    void OnCloseTutorialButtonClicked()
    {
        HideTutorial();
    }

    // Optional: For testing, you might want a way to reset the tutorial
    public void ResetTutorialStatus()
    {
        PlayerPrefs.SetInt(tutorialPlayerPrefKey, 0);
        PlayerPrefs.Save();
        Debug.Log("Tutorial status for " + tutorialPlayerPrefKey + " reset.");
    }

    void OnDestroy()
    {
        // Clean up the listener when the object is destroyed to prevent memory leaks
        if (closeTutorialButton != null)
        {
            closeTutorialButton.onClick.RemoveListener(OnCloseTutorialButtonClicked);
        }
        // Ensure timeScale is reset if the scene is unloaded while paused (e.g., in editor)
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void ToggleTutorial()
    {
        tutorialPanel.SetActive(true);
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
    }
}