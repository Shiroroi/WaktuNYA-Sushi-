using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public SingletonCraftingCanvas singletonCraftingCanvas;
    public static GameManager instance;
    public float distanceToFacePlayer = 15f;

    public List<Sushi> sushiCanCraft;
    public PointerBehaviour pointerBehaviour;
    
    public float moveDuration = 1f;
    public float moveDurationEnd = 1f;
    
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    
    [Header("Aniamtion Ref")]
    public Animator bobAnimator;
    public Animator robotAnimator;
    public Animator chellyAnimator;
    
    private Vector2 AngryCoor = new Vector2(-1, 1);
    private Vector2 HappyCoor = new Vector2(0, 1);
    private Vector2 HeyheyheyCoor = new Vector2(1, 1);
    private Vector2 NormalCoor = new Vector2(-1, -1);
    private Vector2 SadCoor = new Vector2(0, -1);
    private Vector2 SideCoor = new Vector2(1, -1);
    private Vector2 bobDefaultEmoCoor;
    private Vector2 robotDefaultEmoCoor;
    private Vector2 chellyDefaultEmoCoor;
    
    public enum CurrentNpc
    {
        Bob,
        Robot,
        Chelly,
    }
    
    public CurrentNpc currentNpc;
    
    public const string mainMenuSceneName = "MainMenu";

    public Vector2 npcMiddlePosition =  new Vector2(0f, 82f);
    public Vector2 npcEndPosition = new Vector2(1570f, 82f);


    public void SetEmoCoor(Animator targetAnimator, Vector2 emoCoor)
    {
        targetAnimator.SetFloat("EmoX", emoCoor.x);
        targetAnimator.SetFloat("EmoY", emoCoor.y);
        
        Debug.Log("Set animation coor to " + emoCoor);
    }
    
    
    
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
        
        if(SingletonCraftingCanvas.theStaticCraftingCanvas == null)
            singletonCraftingCanvas.HelpCraftingCanvasSingelton();
        
        
    }
    
    
    void Start()
    {
        // change default emotion to corresponding coor
        bobDefaultEmoCoor = SadCoor;
        robotDefaultEmoCoor = NormalCoor;
        chellyDefaultEmoCoor = NormalCoor;
        
        
        
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
                    currentNpc =  CurrentNpc.Bob;
                    pointerBehaviour.sushi.Add(sushiCanCraft[0]);
                    StartCoroutine(npcMovementCoroutine(npc1, position));
                    SetEmoCoor(bobAnimator, SideCoor);
                }
                else
                {
                    
                    StartCoroutine(npcMovementCoroutine_End(npc1, position));
                    SetEmoCoor(bobAnimator, SideCoor);
                }
                
                break;
            
            case 2:
                if (enable == true)
                {
                    npc2.SetActive(true);
                    currentNpc =  CurrentNpc.Robot;
                    pointerBehaviour.sushi.Add(sushiCanCraft[1]);
                    StartCoroutine(npcMovementCoroutine(npc2, position));
                    SetEmoCoor(robotAnimator, SideCoor);
                }
                else
                {
                    
                    StartCoroutine(npcMovementCoroutine_End(npc2, position));
                    SetEmoCoor(robotAnimator, SideCoor);
                }
                break;
            
            case 3:
                if (enable == true)
                {
                    npc3.SetActive(true);
                    currentNpc =  CurrentNpc.Chelly;
                    pointerBehaviour.sushi.Add(sushiCanCraft[2]);
                    StartCoroutine(npcMovementCoroutine(npc3, position));
                    SetEmoCoor(chellyAnimator, SideCoor);
                }
                else 
                {
                    
                    StartCoroutine(npcMovementCoroutine_End(npc3, position));
                    SetEmoCoor(chellyAnimator, SideCoor);
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

                if (npc.CompareTag("npc1"))
                {
                    npc.GetComponentInChildren<Animator>().SetFloat("EmoX", bobDefaultEmoCoor.x);
                    npc.GetComponentInChildren<Animator>().SetFloat("EmoY", bobDefaultEmoCoor.y);
                }
                else if (npc.CompareTag("npc2"))
                {
                    npc.GetComponentInChildren<Animator>().SetFloat("EmoX", robotDefaultEmoCoor.x);
                    npc.GetComponentInChildren<Animator>().SetFloat("EmoY", robotDefaultEmoCoor.y);
                }
                else if (npc.CompareTag("npc3"))
                {
                    npc.GetComponentInChildren<Animator>().SetFloat("EmoX", chellyDefaultEmoCoor.x);
                    npc.GetComponentInChildren<Animator>().SetFloat("EmoY", chellyDefaultEmoCoor.y);
                }
                
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
