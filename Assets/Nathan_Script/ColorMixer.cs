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

            case "White+Blue":
            case "Blue+White":
                return new NamedColor("Sky Blue", new Color(0.5f, 0.8f, 0.9f));

            case "White+Red":
            case "Red+White":
                return new NamedColor("Pink", new Color(1f, 0.75f, 0.8f));

            case "White+Yellow":
            case "Yellow+White":
                return new NamedColor("Pale Yellow", new Color (1f, 1f, 0.5f));

            case "Black+Blue":
            case "Blue+Black":
                return new NamedColor("Dark Blue", new Color(0f, 0f, 0.55f));

            case "Black+Red":
            case "Red+Black":
                return new NamedColor("Dark Red", new Color(0.55f, 0f, 0f));

            case "Black+Yellow":
            case "Yellow+Black":
                return new NamedColor("Olive Green", new Color(0.34f, 0.4f, 0.2f));

            case "Black+White":
            case "White+Black":
                return new NamedColor("Gray", Color.gray);

            default:
                return a; // fallback
        }
    }
}

