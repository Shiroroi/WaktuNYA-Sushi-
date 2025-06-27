using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Core Components")]
    public GameObject window;         // 对话窗口 (带 Sprite Renderer 的那个)
    public TMP_Text dialogueText;     // 显示文本的组件

    [Header("Dialogue Content")]
    public List<string> dialogues;    // 对话内容列表
    public float writingSpeed = 0.05f; // 打字机速度

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

    // --- Update: 处理所有点击输入 ---
    private void Update()
    {
        // 如果对话没开始或没有鼠标点击，则不做任何事
        if (!started || !Input.GetMouseButtonDown(0))
        {
            return;
        }

        // 检查是否点击了任何UI元素 (比如外部的按钮)
        // 如果是，让UI按钮的 OnClick 事件去处理，我们这里就直接返回，避免重复操作
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 如果没点到UI，再检查是否点击到了我们的 Sprite Renderer 背景
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // 如果射线碰到了一个带碰撞体的对象
        if (hit.collider != null)
        {
            // 并且那个对象就是我们的对话窗口
            if (hit.collider.gameObject == window)
            {
                // *** 这里是关键的改动 ***
                // 点击的是背景板，所以也让它继续对话！
                ContinueDialogue();
            }
            else // 如果碰到了其他带碰撞体的对象
            {
                EndDialogue();
            }
        }
        else // 如果射线什么都没碰到 (点击了空白处)
        {
            // 这时才真正关闭对话框
            EndDialogue();
        }
    }


    // --- 核心对话逻辑 (这部分无需改动) ---
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