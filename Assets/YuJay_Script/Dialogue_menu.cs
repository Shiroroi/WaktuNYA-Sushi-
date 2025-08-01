using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;


public class Dialogue_menu : MonoBehaviour
{

    [Header("Core Components")]
    public GameObject window;         
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public string npcName;
    public Button toDinoButton;
    public Button toFishingButton;
    public Button toCyberButton;
    public CinemachineImpulseSource impulseSource;

    [Header("Dialogue Content")]
    public bool canContinue;    
    public List<string> dialogues;    
    public string whenEndPossibleDialogueName_1;
    public string whenEndPossibleDialogueName_2;
    public List<AllPossibleDialogue> allPossibleDialogue;
    [HideInInspector] public string o_whenEndPossibleDialogueName_1;
    [HideInInspector] public string o_whenEndPossibleDialogueName_2;
    [HideInInspector] public int o_listSize;
    public float writingSpeed = 0.05f; 

    private int index;
    private bool started = false;
    private bool isWriting = false;

    [Header("Player react dependency")]
    public GameObject playerReactPanel;
    [HideInInspector] public Button firstChoiceButton;
    public Button secondChoiceButton;
    [HideInInspector] public bool stopBefore;
    [HideInInspector] public bool pauseWhenEnd;

    public float o_musicFadeDuration;
    

    // disable window
    private void Awake()
    {
        window.SetActive(false);
        
    }

    public void Start()
    {
        o_musicFadeDuration = AudioManager.Instance.musicFadeDuration;

        canContinue = true;
        o_whenEndPossibleDialogueName_1 = whenEndPossibleDialogueName_1;
        o_whenEndPossibleDialogueName_2 = whenEndPossibleDialogueName_2;
        
        o_listSize = dialogues.Count;
        pauseWhenEnd = true;


    }

    void OnEnable()
    {
        
        AudioManager.Instance.musicFadeDuration = o_musicFadeDuration;
        if (gameObject.CompareTag("npc1"))
        {
            AudioManager.Instance.PlayMusic("Main_Bgm when npc is npc 1");
        }
        else if (gameObject.CompareTag("npc2"))
        {
            AudioManager.Instance.PlayMusic("Main_Bgm when npc is npc 2");
        }
        else if (gameObject.CompareTag("npc3"))
        {
            AudioManager.Instance.PlayMusic("Main_Bgm when npc is npc 3");
        }
        
        
    }

    // when every frame
    private void Update() 
    {
        
        
        // first two return condition is use to return when im not clicking
        if (  !started || !Input.GetMouseButtonDown(0))  
        {
            

            return; // when i havent start, or when i havent press down my left mouse
            
            // only when I have started the talking and I have click the right mouse only will going down
            
            // can filter alot of unused frame
        }

        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            // prevent keep repeating the first dialogue
            
            // because the in the next frame immediatley will activate the continue dialogue bla bla bla...
            return;
        }

        // get mouse position in world
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // raycast at this point
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        
        // if hit a collider
        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject == window) // collider is our window
            {
                ContinueDialogue(); // speak, got different behaviour inside
            }
            // else  // collider is not our window
            // {
            //     Debug.Log("End1");
            //     EndDialogue(); // end the talking 
            // }
        }
        // else // does not hit a colldier
        // {
        //     Debug.Log("End2");
        //     EndDialogue(); // end the talking
        // }
    }
    // main control function
    public void ContinueDialogue() // when the button press
    {
        
        if (gameObject.CompareTag("npc1"))
        {
            AudioManager.Instance.PlaySfx("Main_When click npc1 to talk");
        }
        else if (gameObject.CompareTag("npc2"))
        {
            AudioManager.Instance.PlaySfx("Main_When click npc2 to talk");
        }
        else if (gameObject.CompareTag("npc3"))
        {
            AudioManager.Instance.PlaySfx("Main_When click npc3 to talk");
        }
        
        if(canContinue == false)
        {
            window.SetActive(!window.activeSelf);
            return;
        }

        if (!started)
        {
            StartDialogue(); // no start before, i will behave like start
        }
        else if (isWriting)
        {
            CompleteCurrentLine(); // when im writting but you still press the button, directly complete the line
        }
        else
        {
            NextDialogue(); // show up next dialogue
        }
    }
    private void StartDialogue()
    {
        // Debug.Log("Start");
        started = true;
        isWriting = true;
        index = 0;
        window.SetActive(true);
        StartCoroutine(Writing(dialogues[index]));
    }
    private void NextDialogue()
    {
        if (canContinue == true && window.activeSelf == false)
        {
            window.SetActive(true);
            
        }

        if (index < dialogues.Count - 1)
        {
            
            //  comment effect { } = - : ;
            
            //  { , after the string start or end with { is being press, will change corressponding level to story mode
            //  } , when the string that start or end with } is typing, will immedieately end change the level back to normal mode and use story item
                    // the stroy item for dino and fishing level is same. so start or end is not important
                    // whereas for cyber level, start with } will use pc mouse, end with } will use realmouse, see line 211 and 233
                    
            //  = , after the string start or end with = is being press, will show customer to npc name
            //  - , after the string start or end with - is being press, will shake the camera
            //  % , after the string start or end with % is being press, will add bracelet to inventory
            //  : , after the string end with : is being press, canContinue will truns into false
            //  ; , if current string end with ; and canContinue is false but i still press, will just close and open current dialogue until canContinue is true
            //
            //  if i say being press means, the dialogue showing current string, and when i try to press to show the next string
            //  element 0 punya comment will execute after player choosing the option, so use comment that is after pressing only got effect
            //  not put ; on element 0
            //  not put comment on the last element in list (not last char in string) except }

            if (string.IsNullOrEmpty(dialogues[index]) == false && (dialogues[index].EndsWith("{")||dialogues[index].StartsWith("{")))
            {
                
                if (gameObject.CompareTag("npc1"))
                {
                    toDinoButton.onClick.RemoveAllListeners();
                    toDinoButton.onClick.AddListener(() => {toDinoButton.GetComponent<ChangeToNewScene>().ChangeSceneToDinoStory(); });
                }
                else if (gameObject.CompareTag("npc2"))
                {
                    toCyberButton.onClick.RemoveAllListeners();

                    toCyberButton.onClick.AddListener(() => {toCyberButton.GetComponent<ChangeToNewScene>().ChangeSceneToCyberStory(); });
                }
                else if (gameObject.CompareTag("npc3"))
                {
                    toFishingButton.onClick.RemoveAllListeners();

                    toFishingButton.onClick.AddListener(() => {toFishingButton.GetComponent<ChangeToNewScene>().ChangeSceneToFishingStory(); });
                }
            }
            
            if (string.IsNullOrEmpty(dialogues[index+1]) == false && dialogues[index+1].EndsWith("}") )
            {
                if (gameObject.CompareTag("npc1"))
                {
                    toDinoButton.onClick.RemoveAllListeners();
                    toDinoButton.onClick.AddListener(() => {toDinoButton.GetComponent<ChangeToNewScene>().ChangeSceneToDino(); });
                    InventoryManager.instance.UseItemByName("rock");
                }
                else if (gameObject.CompareTag("npc2"))
                {
                    toCyberButton.onClick.RemoveAllListeners();
                    toCyberButton.onClick.AddListener(() => {toCyberButton.GetComponent<ChangeToNewScene>().ChangeSceneToCyber(); });
                    InventoryManager.instance.UseItemByName("realMouse");
                }
                else if (gameObject.CompareTag("npc3"))
                {
                    toFishingButton.onClick.RemoveAllListeners();
                    toFishingButton.onClick.AddListener(() => {toFishingButton.GetComponent<ChangeToNewScene>().ChangeSceneToFishing(); });
                    InventoryManager.instance.UseItemByName("mermaid");
                }
            }
            
            if (string.IsNullOrEmpty(dialogues[index+1]) == false && dialogues[index+1].StartsWith("}"))
            {
                if (gameObject.CompareTag("npc1"))
                {
                    toDinoButton.onClick.RemoveAllListeners();
                    toDinoButton.onClick.AddListener(() => {toDinoButton.GetComponent<ChangeToNewScene>().ChangeSceneToDino(); });
                    InventoryManager.instance.UseItemByName("rock");
                }
                else if (gameObject.CompareTag("npc2"))
                {
                    toCyberButton.onClick.RemoveAllListeners();
                    toCyberButton.onClick.AddListener(() => {toCyberButton.GetComponent<ChangeToNewScene>().ChangeSceneToCyber(); });
                    InventoryManager.instance.UseItemByName("pcMouse");
                }
                else if (gameObject.CompareTag("npc3"))
                {
                    toFishingButton.onClick.RemoveAllListeners();
                    toFishingButton.onClick.AddListener(() => {toFishingButton.GetComponent<ChangeToNewScene>().ChangeSceneToFishing(); });
                    InventoryManager.instance.UseItemByName("mermaid");
                }
            }
            
            if (string.IsNullOrEmpty(dialogues[index]) == false && dialogues[index].StartsWith("="))
            {
                nameText.text = npcName;
            }
            
            if (string.IsNullOrEmpty(dialogues[index]) == false && dialogues[index].EndsWith("="))
            {
                nameText.text = npcName;
            }
            
            if (string.IsNullOrEmpty(dialogues[index]) == false && (dialogues[index].EndsWith("-")||dialogues[index].StartsWith("-")))
            {
                impulseSource.GenerateImpulse();
            }

            if (string.IsNullOrEmpty(dialogues[index]) == false && (dialogues[index].EndsWith("%") || dialogues[index].StartsWith("%")))
            {
                InventoryManager.instance.AddItem("bracelet");
            }
            
            if (string.IsNullOrEmpty(dialogues[index]) == false && dialogues[index].EndsWith(":"))
            {
                canContinue = false;
            }
            
            // i want to stop, but got condition
            if (string.IsNullOrEmpty(dialogues[index]) == false && dialogues[index].EndsWith(";") && canContinue == false)
            {
                if (stopBefore == true && canContinue == true)
                {
                    stopBefore = false;
                }
                else if (stopBefore == false)
                {
                    stopBefore = true;
                    window.SetActive(false);
                    return;
                }
            }
        }
        
        
        ++ index;
        
        if (index < dialogues.Count)
        {
            isWriting = true;
            StartCoroutine(Writing(dialogues[index]));
        }
        else if (index == dialogues.Count && whenEndPossibleDialogueName_1 != null && whenEndPossibleDialogueName_2 != null && whenEndPossibleDialogueName_1 != "" && whenEndPossibleDialogueName_2 != "") 
        {
            playerReactPanel.SetActive(true);
            
            firstChoiceButton.onClick.RemoveAllListeners();
            secondChoiceButton.onClick.RemoveAllListeners();
            
            firstChoiceButton.onClick.AddListener(
                () =>
            {
                AddDialogueToButton(whenEndPossibleDialogueName_1);
                ContinueDialogue();
            }
                );
            secondChoiceButton.onClick.AddListener(
                () =>
            {
                AddDialogueToButton(whenEndPossibleDialogueName_2);
                ContinueDialogue();
            }
                );
            
            firstChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = whenEndPossibleDialogueName_1;
            secondChoiceButton.GetComponentInChildren<TextMeshProUGUI>().text = whenEndPossibleDialogueName_2;
            

            // player multiple option dialogue
            // at the same time, the dialogue will just stay there
        }
        else
        {
            EndDialogue();
        }
    }
    private void CompleteCurrentLine()
    {
        StopAllCoroutines();
        dialogueText.text = dialogues[index];
        isWriting = false;
    }
    public void EndDialogue() // when the npc turns end
    {

        isWriting = false;
        StopAllCoroutines();
        window.SetActive(false);


        if (pauseWhenEnd == true)
        {
            pauseWhenEnd = false;
            return;
        }
            
        
        started = false;
        
        
        Debug.Log("Should change character");
        whenEndPossibleDialogueName_1 = o_whenEndPossibleDialogueName_1;
        whenEndPossibleDialogueName_2 = o_whenEndPossibleDialogueName_2;
        
        dialogues.RemoveRange(o_listSize,dialogues.Count - o_listSize);
        
        

        
        
        

        if (gameObject.CompareTag("npc1"))
        {
            GameManager.instance.EnableNpc(1, false, GameManager.instance.npcEndPosition);
            GameManager.instance.EnableNpc(2, true, GameManager.instance.npcMiddlePosition);
            
        }
        else if (gameObject.CompareTag("npc2"))
        {
            GameManager.instance.EnableNpc(2, false, GameManager.instance.npcEndPosition);
            GameManager.instance.EnableNpc(3, true, GameManager.instance.npcMiddlePosition);
            
        }
        else if (gameObject.CompareTag("npc3"))
        {
            GameManager.instance.EnableNpc(3, false, GameManager.instance.npcEndPosition);
        }
            
        
    }
    private IEnumerator Writing(string currentDialogue)
    {
        currentDialogue = currentDialogue.Replace(";", "");
        currentDialogue = currentDialogue.Replace("{", "");
        currentDialogue = currentDialogue.Replace("}", "");
        currentDialogue = currentDialogue.Replace("=", "");
        currentDialogue = currentDialogue.Replace("-", "");
        currentDialogue = currentDialogue.Replace(":", "");
        currentDialogue = currentDialogue.Replace("%", "");
        
        if (gameObject.CompareTag("npc1"))
        {
            AudioManager.Instance.PlaySfx(true,"Main_When npc1 talk", "Main_When npc1 talk v2");
        }
        else if (gameObject.CompareTag("npc2"))
        {
            AudioManager.Instance.PlaySfx(true,"Main_When npc2 talk");
        }
        else if (gameObject.CompareTag("npc3"))
        {
            AudioManager.Instance.PlaySfx(true, "Main_When npc3 talk");
        }
        
        
        
        isWriting = true;
        dialogueText.text = "";
        
        foreach (char letter in currentDialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }

        isWriting = false;
    }

    public void AddDialogueToButton(string whenEndPossibleDialogueName)
    {
        bool finded = false;
   
        foreach (AllPossibleDialogue possibleDialogue in allPossibleDialogue)
        {
            
            if (possibleDialogue.dialogueName == whenEndPossibleDialogueName)
            {
                finded = true;

                foreach (string line in possibleDialogue.dialogue_LEAVE_A_BLANK_ON_FIRST_ONE)
                {
                    dialogues.Add(line);
                }

                whenEndPossibleDialogueName_1 = possibleDialogue.whenEndPossibleDialogueName_1;
                whenEndPossibleDialogueName_2 = possibleDialogue.whenEndPossibleDialogueName_2;
                
                break;
            }
            
        }

        if (finded == false)
        {
            // Debug.Log("Cant find possible dialogue with the name");
        }
        else
        {
            // Debug.Log("Find the possibe dialogue");
        }
    }

    

}