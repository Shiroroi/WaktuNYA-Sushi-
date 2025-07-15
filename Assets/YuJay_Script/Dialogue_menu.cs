using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Dialogue_menu : MonoBehaviour
{
    [Header("Core Components")]
    public GameObject window;         
    public TMP_Text dialogueText;     

    [Header("Dialogue Content")]
    public List<string> dialogues;    
    public float writingSpeed = 0.05f; 

    private int index;
    private bool started = false;
    private bool isWriting = false;

    private void Awake()
    {
        ToggleWindow(false);
    }

    // --- 主控制方法 ---
    public void ContinueDialogue()
    {
        if (!started)
        {
            StartDialogue();
        }
        else if (isWriting)
        {
            CompleteCurrentLine();
        }
        else
        {
            NextDialogue();
        }
    }

    public void EndDialogue()
    {
        started = false;
        isWriting = false;
        StopAllCoroutines();
        ToggleWindow(false);
    }

    
    private void Update()
    {
        
        if (!started || !Input.GetMouseButtonDown(0))
        {
            return;
        }

        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        
        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject == window)
            {
                
                ContinueDialogue();
            }
            else 
            {
                EndDialogue();
            }
        }
        else 
        {
            
            EndDialogue();
        }
    }


    
    private void StartDialogue()
    {
        started = true;
        isWriting = true;
        index = 0;
        ToggleWindow(true);
        StartCoroutine(Writing(dialogues[index]));
    }

    private void NextDialogue()
    {
        index++;
        if (index < dialogues.Count)
        {
            isWriting = true;
            StartCoroutine(Writing(dialogues[index]));
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

    private IEnumerator Writing(string currentDialogue)
    {
        isWriting = true;
        dialogueText.text = "";

        foreach (char letter in currentDialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }

        isWriting = false;
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
}