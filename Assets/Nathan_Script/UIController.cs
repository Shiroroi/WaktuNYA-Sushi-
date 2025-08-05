using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject balatoPanel;
    public GameObject recipePanel;
    public GameObject inventoryPanel;

    public Transform inventoryTransform;
    public Transform inventoryOriginalParent;
    public Transform inventoryCraftingParent;

    public DOTweenUIAnimator pausePanelAnimator;
    public DOTweenUIAnimator balatoPanelAnimator;
    public DOTweenUIAnimator recipePanelAnimator;
   // public DOTweenUIAnimator inventoryPanelAnimator;





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
        
        

        
    }

    public void TogglePause()
    {
        //isPauseOpen = !isPauseOpen;
        //if(this !=null)
        pausePanel.SetActive(true);
        pausePanelAnimator.OpenUI();

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
        balatoPanelAnimator.OpenUI();

        //if (isJournalOpen && isPauseOpen)
        //{
        //    pausePanel.SetActive(false);
        //    isPauseOpen = false;
        //}
    }

    public void ToggleRecipe()
    {
        recipePanel.SetActive(true);
        recipePanelAnimator.OpenUI();
        AudioManager.Instance.sfxVolumeScale = 7f;
        AudioManager.Instance.PlaySfx("Main_When click recipe button");
        AudioManager.Instance.sfxVolumeScale = 1f;

        //if (isJournalOpen && isPauseOpen)
        //{
        //    pausePanel.SetActive(false);
        //    balatoPanel.SetActive(false );
        //    isPauseOpen = false;
        //    isJournalOpen = false;
        //}
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(true);
        //inventoryPanelAnimator.OpenUI();
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
        AudioManager.Instance.sfxVolumeScale = 8f;
        AudioManager.Instance.PlaySfx("Main_When press crafting table button");
        AudioManager.Instance.sfxVolumeScale = 1f;
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

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
}