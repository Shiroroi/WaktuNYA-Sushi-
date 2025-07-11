using UnityEngine;
using System.Collections.Generic;

public static class ColorMixer
{
    public static NamedColor Mix(NamedColor a, NamedColor b)
    {
        string key = a.colorName + "+" + b.colorName;
        string keyRev = b.colorName + "+" + a.colorName;

        switch (key)
        {
            case "Red+Yellow":
            case "Yellow+Red":
                return new NamedColor("Orange", new Color(1f, 0.5f, 0f));

            case "Blue+Yellow":
            case "Yellow+Blue":
                return new NamedColor("Green", Color.green);

            case "Red+Blue":
            case "Blue+Red":
                return new NamedColor("Magenta", Color.magenta);

            default:
                return a; // fallback
        }
    }
}

