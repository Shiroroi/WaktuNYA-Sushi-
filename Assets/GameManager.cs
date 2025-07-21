using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public SingletonCraftingCanvas singletonCraftingCanvas;
    [HideInInspector] public GameManager instance;

    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    
    public const string dinoeSceneName = "Dino Level";
    public const string fishingSceneName = "YuJay_Fishing";
    public const string cyberpunkSceneName = "CyberpunkLevel";
    public const string mainMenuSceneName = "MainMenu";
    
    public enum PlayMode
    {
        Debug,
        Play
    }
    
    private PlayMode o_playMode;
    public PlayMode playMode;
    
    public static Level currentCevel;
    
    
    void Awake()
    {
        o_playMode  = playMode;
            
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        if(SingletonCharacterCanvas.theStaticChracterCanvas == null)
            singletonCraftingCanvas.HelpCraftingCanvasSingelton();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playMode != o_playMode)
            playMode = o_playMode;
        
        
        switch (playMode)
        {
            case PlayMode.Debug:
                DebugUpdate();
                break;
            
            case PlayMode.Play:
                PlayUpdate();
                break;
        }
    }

    void DebugUpdate()
    {
        
    }

    void PlayUpdate()
    {
        CheckSceneType(); // check what scene type is now, auto udate

        switch (currentCevel.type)
        {
            case Level.Type.dino:
                break;
            
            case Level.Type.fishing:
                break;
            
            case Level.Type.cyberpunk:
                break;
            
            case Level.Type.mainMenu:
                break;
        }
    }

    // let other class change level and progress
    public void ChangeLevelProgress(Level.Type type, Level.Progress progress)
    {
        switch (type)
        {
            case Level.Type.dino:
                break;
            
            case Level.Type.fishing:
                break;
            
            case Level.Type.cyberpunk:
                break;
            
            case Level.Type.mainMenu:
                break;
        }
    }
    private void CheckSceneType()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case dinoeSceneName:
                currentCevel.type =  Level.Type.dino;
                break;
            
            case fishingSceneName:
                currentCevel.type =  Level.Type.fishing;
                break;
            
            case cyberpunkSceneName:
                currentCevel.type =  Level.Type.cyberpunk;
                break;
            
            case mainMenuSceneName:
                currentCevel.type =  Level.Type.mainMenu;
                break;
        }
    }

    public void EnableNpc(int npcWhat, bool enable, Vector2 position )
    {
        switch (npcWhat)
        {
            case 1:
                npc1.SetActive(enable);
                npc2.GetComponent<RectTransform>().position = position;
                break;
            
            case 2:
                npc2.SetActive(enable);
                npc2.GetComponent<RectTransform>().position = position;
                break;
            
            case 3:
                npc3.SetActive(enable);
                npc3.GetComponent<RectTransform>().position = position;
                break;
        }
    }
    
    
}
