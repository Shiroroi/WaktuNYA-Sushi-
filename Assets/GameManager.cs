using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public SingletonCraftingCanvas singletonCraftingCanvas;
    [HideInInspector] public static GameManager instance;

    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    
    public const string dinoeSceneName = "Dino Level";
    public const string fishingSceneName = "YuJay_Fishing";
    public const string cyberpunkSceneName = "CyberpunkLevel";
    public const string mainMenuSceneName = "MainMenu";

    public Vector2 npcMiddlePosition =  new Vector2(0, 82);
    public enum PlayMode
    {
        Debug,
        Play
    }
    
    private PlayMode o_playMode;
    public PlayMode playMode;
    
    public Level currentCevel;
    
    
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
        EnableNpc(1, true, npcMiddlePosition);
        EnableNpc(2, false, npcMiddlePosition);
        EnableNpc(3, false, npcMiddlePosition);
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
                npc1.GetComponent<RectTransform>().localPosition = position;
                break;
            
            case 2:
                npc2.SetActive(enable);
                npc2.GetComponent<RectTransform>().localPosition = position;
                break;
            
            case 3:
                npc3.SetActive(enable);
                npc3.GetComponent<RectTransform>().localPosition = position;
                break;
        }
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
