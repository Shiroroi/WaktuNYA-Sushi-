using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Dialogue_menu : MonoBehaviour
{

    [Header("Core Components")]
    public GameObject window;         
    public TMP_Text dialogueText;     

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
    

    // disable window
    private void Awake()
    {
        window.SetActive(false);
    }

    public void Start()
    {
        canContinue = true;
        o_whenEndPossibleDialogueName_1 = whenEndPossibleDialogueName_1;
        o_whenEndPossibleDialogueName_2 = whenEndPossibleDialogueName_2;
        
        o_listSize = dialogues.Count;
        
        
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
        
        // i want to stop, but got condition
        if (string.IsNullOrEmpty(dialogues[index]) == false && dialogues[index].EndsWith(";"))
        {
            if (stopBefore == true && canContinue == true)
            {
                stopBefore = false;
            }
            else if (stopBefore == false)
            {
                stopBefore = true;
                canContinue = false;
                window.SetActive(false);
                return;
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
    public void EndDialogue()
    {
        whenEndPossibleDialogueName_1 = o_whenEndPossibleDialogueName_1;
        whenEndPossibleDialogueName_2 = o_whenEndPossibleDialogueName_2;
        
        dialogues.RemoveRange(o_listSize,dialogues.Count - o_listSize);
        
        started = false;
        isWriting = false;
        StopAllCoroutines();
        window.SetActive(false);
    }
    private IEnumerator Writing(string currentDialogue)
    {
        currentDialogue = currentDialogue.Replace(";", "");
        
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