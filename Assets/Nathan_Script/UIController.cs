using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject balatoPanel;
    public GameObject recipePanel;

    public Transform inventoryTransform;
    public Transform inventoryOriginalParent;
    public Transform inventoryCraftingParent;

    public GameObject startPanel;

    public Button closeStartPanelButton;

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

    void Start()
    {
        
        Time.timeScale = 0f;

        // Add a listener to the close button
        if (closeStartPanelButton != null)
        {
            closeStartPanelButton.onClick.AddListener(OnCloseAnnouncementButtonClicked);
        }
    }

    public void TogglePause()
    {
        //isPauseOpen = !isPauseOpen;
        //if(this !=null)
        pausePanel.SetActive(true);

        // Optional: close journal if open
        //if (isPauseOpen && isJournalOpen)
        //{
        //    balatoPanel.SetActive(false);
        //    isJournalOpen = false;
        //}
    }

    public void ToggleBalato()
    {
        //isJournalOpen = !isJournalOpen;
        balatoPanel.SetActive(true);

        //if (isJournalOpen && isPauseOpen)
        //{
        //    pausePanel.SetActive(false);
        //    isPauseOpen = false;
        //}
    }

    public void ToggleRecipe()
    {
        recipePanel.SetActive(true);

        //if (isJournalOpen && isPauseOpen)
        //{
        //    pausePanel.SetActive(false);
        //    balatoPanel.SetActive(false );
        //    isPauseOpen = false;
        //    isJournalOpen = false;
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPauseOpen || isJournalOpen)
            {
                pausePanel.SetActive(false);
                balatoPanel.SetActive(false);
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

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
    }

    public void CloseBalatoPanel()
    {
        balatoPanel.SetActive(false);
    }

    public void CloseRecipePanel()
    {
        recipePanel.SetActive(false);
    }

    public void OnCloseAnnouncementButtonClicked()
    {
        Debug.Log("Announcement panel closed.");

        if (startPanel != null)
        {
            startPanel.SetActive(false); 
        }

        Time.timeScale = 1f;
    }
}