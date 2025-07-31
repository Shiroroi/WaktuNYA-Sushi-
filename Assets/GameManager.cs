using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public SingletonCraftingCanvas singletonCraftingCanvas;
    public static GameManager instance;
    public float distanceToFacePlayer = 15f;
    
    public float moveDuration = 1f;
    public float moveDurationEnd = 1f;
    
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;

    
    
    public const string mainMenuSceneName = "MainMenu";

    public Vector2 npcMiddlePosition =  new Vector2(0f, 82f);
    public Vector2 npcEndPosition = new Vector2(1570f, 82f);
    
    
    
    
    
    
    void Awake()
    {
        
            
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
    
    
    void Start()
    {
        EnableNpc(1, true, npcMiddlePosition);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    
    
    
      
    

    public void EnableNpc(int npcWhat, bool enable, Vector2 position )
    {
        switch (npcWhat)
        {
            case 1:
                if (enable == true)
                {
                    npc1.SetActive(true);
                    
                    StartCoroutine(npcMovementCoroutine(npc1, position));
                }
                else
                {
                    
                    StartCoroutine(npcMovementCoroutine_End(npc1, position));
                }
                
                break;
            
            case 2:
                if (enable == true)
                {
                    npc2.SetActive(true);
                    
                    StartCoroutine(npcMovementCoroutine(npc2, position));
                }
                else
                {
                    
                    StartCoroutine(npcMovementCoroutine_End(npc2, position));
                }
                break;
            
            case 3:
                if (enable == true)
                {
                    npc3.SetActive(true);
                    
                    StartCoroutine(npcMovementCoroutine(npc3, position));
                }
                else 
                {
                    
                    StartCoroutine(npcMovementCoroutine_End(npc3, position));
                }
                
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
    
    private IEnumerator npcMovementCoroutine(GameObject npc, Vector2 position)
    {
        // Npc side face to player
        float currentTime = 0f;
        RectTransform npcT = npc.GetComponent<RectTransform>();
        
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            npcT.localPosition = Vector2.Lerp(npcT.localPosition, position, Mathf.Clamp01(currentTime / moveDuration));

            
            if (Vector2.Distance(npcT.localPosition, position) < distanceToFacePlayer)
            {
                // Npc should face to player
                npc.GetComponent<Button>().enabled = true;
                npcT.localPosition = position;
                break;
            }
            yield return null;
        }
        
        
    }
    
    private IEnumerator npcMovementCoroutine_End(GameObject npc, Vector2 position)
    {
        // Npc side face to player
        npc.GetComponent<Button>().enabled = false;
        float currentTime = 0f;
        RectTransform npcT = npc.GetComponent<RectTransform>();
        
        while (currentTime < moveDurationEnd)
        {
            currentTime += Time.deltaTime;
            npcT.localPosition = Vector2.Lerp(npcT.localPosition, position, Mathf.Clamp01(currentTime / moveDurationEnd));
            
            if (Vector2.Distance(npcT.localPosition, position) < 10f)
            {
                npcT.localPosition = position;
                break;
            }
            
            yield return null;
        }
        
        npc.SetActive(false);
    }


}
