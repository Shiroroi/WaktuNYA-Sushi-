using System;
using UnityEngine;

[Serializable]
public class Level
{
 
    public enum Type
    {
        dino,
        dino_s,
        fishing,
        fishing_s,
        cyberpunk,
        cyberpunk_s,
        mainMenu
    }
    
    
    public Type type;
}
