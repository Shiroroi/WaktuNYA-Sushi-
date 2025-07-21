using System;
using UnityEngine;

[Serializable]
public class Level
{
    public enum Progress
    {
        tutorial,
        story,
        normal
    }

    public enum Type
    {
        dino,
        fishing,
        cyberpunk,
        mainMenu
    }
    
    public Progress progress;
    public Type type;
}
