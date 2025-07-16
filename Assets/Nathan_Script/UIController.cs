using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject journalPanel;

    public Transform inventoryTransform;
    public Transform inventoryOriginalParent;
    public Transform inventoryCraftingParent;

    private bool isPauseOpen = false;
    private bool isJournalOpen = false;

    public static UIController instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
            
        else
            Destroy(gameObject);
    
    }
    
    public void TogglePause()
    {
        isPauseOpen = !isPauseOpen;
        if(this !=null)
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

    public void OpenCraftingMenu()
    {
        inventoryTransform.SetParent(inventoryCraftingParent,false);
    }
    public void CloseCraftingMenu()
    {
        inventoryTransform.SetParent(inventoryOriginalParent,false);
    }
}