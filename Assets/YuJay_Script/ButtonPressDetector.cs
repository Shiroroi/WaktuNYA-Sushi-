using UnityEngine;

public class ButtonPressDetector : MonoBehaviour
{
    public Dialogue dialogueScript;

    // 为了清晰，我把函数名也改了，但你也可以不改
    public void OnContinueButtonPressed()
    {
        // 每次点击都调用这个总控方法
        if (dialogueScript != null)
        {
            dialogueScript.ContinueDialogue();
        }
    }
}