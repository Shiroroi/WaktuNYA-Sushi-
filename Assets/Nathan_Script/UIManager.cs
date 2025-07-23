using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum Tool { Pencil, Eraser }
    public Tool currentTool = Tool.Eraser;

    public NamedColor selectedNamedColor;

    public void SelectRed() => selectedNamedColor = new NamedColor("Red", Color.red);
    public void SelectYellow() => selectedNamedColor = new NamedColor("Yellow", Color.yellow);
    public void SelectBlue() => selectedNamedColor = new NamedColor("Blue", Color.blue);

    public void SelectTool(int toolId)
    {
        currentTool = (Tool)toolId;
    }


    private void Start()
    {
        
    }

    public void UseToolOnTile(TileSlot tile)
    {
        if (currentTool == Tool.Pencil)
        {
            
            tile.AddColor(selectedNamedColor);
        }
        else if (currentTool == Tool.Eraser)
        {
            
            tile.EraseColor();
        }
        

        
    }
}

