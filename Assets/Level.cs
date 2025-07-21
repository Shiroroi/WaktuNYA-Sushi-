using System;
using UnityEngine;

[Serializable]
public class Level
{
 
    public enum Type
    {
        dino,
        fishing,
        cyberpunk,
        mainMenu
    }
    
    
    public Type type;
}
