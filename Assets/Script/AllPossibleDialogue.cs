using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AllPossibleDialogue // each element of AllPosibleDialogue will have following properties
{
    public string dialogueName;
    public List<string> dialogue_LEAVE_A_BLANK_ON_FIRST_ONE;
    public string whenEndPossibleDialogueName_1;
    public string whenEndPossibleDialogueName_2;
}
