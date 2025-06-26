using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public TileSlot[] tiles;
    public TextMeshProUGUI guideBookText;

    private Color[] currentRecipe = new Color[3];

    void Start()
    {
        // Set a sample recipe: Green, Orange, Orange (Carrot)
        currentRecipe[0] = Color.green;
        currentRecipe[1] = new Color(1f, 0.5f, 0f); // Orange
        currentRecipe[2] = new Color(1f, 0.5f, 0f); // Orange
        guideBookText.text = "Carrot = Green, Orange, Orange";
    }

    public void CheckRecipe()
    {
        bool match = true;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetColor() != currentRecipe[i])
            {
                match = false;
                break;
            }
        }

        if (match)
            Debug.Log("Correct Recipe! You made a Carrot");
        else
            Debug.Log("Incorrect! Try Again");
    }
}
