// TutorialManager.cs
using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Button
using TMPro; // Required if you are using TextMeshPro

public class TutorialManager : MonoBehaviour
{
    // Reference to your tutorial UI Panel GameObject
    public GameObject tutorialPanel;

    // A unique key to identify this specific tutorial in PlayerPrefs
    public string tutorialPlayerPrefKey = "HasShownDinoGameTutorial"; // Example key

    // Reference to the button that closes the tutorial
    public Button closeTutorialButton;

    void Start()
    {
        // Check if the tutorial has already been shown
        // PlayerPrefs.GetInt returns 0 if the key doesn't exist, or the stored integer value.
        // We use 1 for "shown" and 0 for "not shown".
        if (PlayerPrefs.GetInt(tutorialPlayerPrefKey, 0) == 0)
        {
            // Tutorial has NOT been shown yet, so display it
            ShowTutorial();
        }
        else
        {
            // Tutorial HAS been shown, so ensure the panel is hidden
            HideTutorial();
        }

        // Add a listener to the close button
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
        }
    }

    void OnCloseTutorialButtonClicked()
    {
        // When the close button is clicked, hide the tutorial
        HideTutorial();
    }

    // Optional: For testing, you might want a way to reset the tutorial
    // You could call this from another button or a debug menu.
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
    }
}
