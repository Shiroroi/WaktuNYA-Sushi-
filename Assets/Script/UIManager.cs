using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum Tool { Pencil, Eraser }
    public Tool currentTool = Tool.Pencil;

    public NamedColor selectedNamedColor;

    public GameObject Recipe;
    public GameObject colorGuide;

    public TileSlot[] tiles; // 🔹 Add this to link your tile slots in the Inspector

    // ---- COLOR SELECTION ----
    public void SelectOrange() => SelectColor(new NamedColor("Orange", new Color(1f, 0.5f, 0f)));
    public void SelectDarkRed() => SelectColor(new NamedColor("DarkRed", new Color(0.55f, 0f, 0f)));
    public void SelectGreen() => SelectColor(new NamedColor("Green", Color.green));
    public void SelectWhite() => SelectColor(new NamedColor("White", Color.white));
    public void SelectBlack() => SelectColor(new NamedColor("Black", Color.black));

    private void SelectColor(NamedColor color)
    {
        AudioManager.Instance.PlaySfx("Cyber_When choose color");
        selectedNamedColor = color;
        AutoFillNextTile();
    }

    // ---- AUTO FILL LOGIC ----
    private void AutoFillNextTile()
    {
        foreach (TileSlot tile in tiles)
        {
            if (!tile.HasColor()) // 🔹 Assuming TileSlot has a method or bool to check if it’s empty
            {
                AudioManager.Instance.PlaySfx("Cyber_When putting color");
                tile.AddColor(selectedNamedColor);
                return; // Fill only one tile per click
            }
        }
    }

    // ---- ERASE EVERYTHING ----
    public void SelectTool(int toolId)
    {
        AudioManager.Instance.PlaySfx("Cyber_When choose color");
        currentTool = (Tool)toolId;

        if (currentTool == Tool.Eraser)
        {
            EraseAllTiles();
        }
    }

    private void EraseAllTiles()
    {
        AudioManager.Instance.PlaySfx("Cyber_When erase color");
        foreach (TileSlot tile in tiles)
        {
            tile.EraseColor();
        }
    }

    // ---- UI PANELS ----
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
