using UnityEngine;


[System.Serializable]
public class NamedColor
{
    public string colorName;
    public Color colorValue;

    public NamedColor(string name, Color value)
    {
        colorName = name;
        colorValue = value;
    }
}

