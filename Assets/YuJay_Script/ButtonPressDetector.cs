using UnityEngine;

public class ButtonPressDetector : MonoBehaviour
{
    public Dialogue dialogueScript;

    
    public void OnContinueButtonPressed()
    {
        // Every time click use the function
        if (dialogueScript != null)
        {
            dialogueScript.ContinueDialogue();
        }
    }
}