using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum Tool { Pencil, Eraser }
    public Tool currentTool = Tool.Pencil;

    public NamedColor selectedNamedColor;

    public void SelectRed() => selectedNamedColor = new NamedColor("Red", Color.red);
    public void SelectYellow() => selectedNamedColor = new NamedColor("Yellow", Color.yellow);
    public void SelectBlue() => selectedNamedColor = new NamedColor("Blue", Color.blue);

    public void SelectTool(int toolId)
    {
        currentTool = (Tool)toolId;
    }

    public void UseToolOnTile(TileSlot tile)
    {
        if (currentTool == Tool.Pencil)
            tile.AddColor(selectedNamedColor);
        else
            tile.EraseColor();
    }
}

