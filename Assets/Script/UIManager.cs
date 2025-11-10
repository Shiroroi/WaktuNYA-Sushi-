using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum Tool { Pencil, Eraser }
    public Tool currentTool = Tool.Pencil;

    public NamedColor selectedNamedColor;

    public GameObject Recipe;
    public GameObject colorGuide;

    public void SelectOrange() => selectedNamedColor = new NamedColor("Orange", new Color(1f, 0.5f, 0f));
    public void SelectDarkRed() => selectedNamedColor = new NamedColor("DarkRed", new Color(0.55f, 0f, 0f));
    public void SelectGreen() => selectedNamedColor = new NamedColor("Green", Color.green);
    public void SelectWhite() => selectedNamedColor = new NamedColor("White", Color.white);
    public void SelectBlack() => selectedNamedColor = new NamedColor("Black", Color.black);


    public void SelectTool(int toolId)
    {
        AudioManager.Instance.PlaySfx("Cyber_When choose color");

        currentTool = (Tool)toolId;
    }

    public void UseToolOnTile(TileSlot tile)
    {
        if (currentTool == Tool.Pencil)
        {
            AudioManager.Instance.PlaySfx("Cyber_When putting color");

            tile.AddColor(selectedNamedColor);
        }
        if (currentTool == Tool.Eraser)
        {
            AudioManager.Instance.PlaySfx("Cyber_When erase color");

            tile.EraseColor();
        }
            
    }

    public void ToggleRecipe()
    {
        AudioManager.Instance.PlaySfx("Menu_Button click sound");
        Recipe.SetActive(true);
    }

    public void CloseRecipe()
    {
        AudioManager.Instance.PlaySfx("Menu_Button click sound");
        Recipe.SetActive(false);
    }

    public void ToggleColorGuide()
    {
        AudioManager.Instance.PlaySfx("Menu_Button click sound");
        colorGuide.SetActive(true);
    }

    public void CloseColorGuide()
    {
        AudioManager.Instance.PlaySfx("Menu_Button click sound");
        colorGuide.SetActive(false);
    }
    

}

