using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TileSlot : MonoBehaviour
{
    public Image tileImage;

    private List<NamedColor> stack = new List<NamedColor>();

    public void AddColor(NamedColor color)
    {
        if (stack.Count == 0)
        {
            stack.Add(color);
            tileImage.color = color.colorValue;
        }
        else if (stack.Count == 1)
        {
            stack.Add(color);
            var mixed = ColorMixer.Mix(stack[0], stack[1]);
            stack.Clear();
            stack.Add(mixed);
            tileImage.color = mixed.colorValue;
        }
        else
        {
            stack.Clear();
            stack.Add(color);
            tileImage.color = color.colorValue;
        }
    }

    public void EraseColor()
    {
        stack.Clear();
        tileImage.color = new Color(0, 0, 0, 0.294f); // semi-transparent background
    }

    public Color GetColor()
    {
        return tileImage.color;
    }

    // 🔹 New method for UIManager auto-fill
    public bool HasColor()
    {
        // If the alpha is greater than 0.3, assume it's filled
        return tileImage.color.a > 0.3f;
    }
}