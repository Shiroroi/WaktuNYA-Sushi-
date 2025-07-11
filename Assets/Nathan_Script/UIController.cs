using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject journalPanel;

    private bool isPauseOpen = false;
    private bool isJournalOpen = false;

    public void TogglePause()
    {
        isPauseOpen = !isPauseOpen;
        pausePanel.SetActive(isPauseOpen);

        // Optional: close journal if open
        if (isPauseOpen && isJournalOpen)
        {
            journalPanel.SetActive(false);
            isJournalOpen = false;
        }
    }

    public void ToggleJournal()
    {
        isJournalOpen = !isJournalOpen;
        journalPanel.SetActive(isJournalOpen);

        if (isJournalOpen && isPauseOpen)
        {
            pausePanel.SetActive(false);
            isPauseOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPauseOpen || isJournalOpen)
            {
                pausePanel.SetActive(false);
                journalPanel.SetActive(false);
                isPauseOpen = false;
                isJournalOpen = false;
            }
            else
            {
                TogglePause();
            }
        }
    }
}