using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public Color[] colors = new Color[3];
    public Sprite resultSprite;
}

public class RecipeManager : MonoBehaviour
{
    public TileSlot[] tiles;
    //public TextMeshProUGUI guideBookText;

    public List<Recipe> allRecipes;

    [Header("Popup UI")]
    public GameObject resultPopupPanel;
    public TextMeshProUGUI resultText;
    public Image resultImage;

    void Start()
    {
        // Display all recipes in the guidebook
        //guideBookText.text = "";
        //foreach (var recipe in allRecipes)
        //{
        //    guideBookText.text += $"{recipe.recipeName} = {ColorName(recipe.colors[0])}, {ColorName(recipe.colors[1])}, {ColorName(recipe.colors[2])}\n";
        //}
    }

    public void CheckRecipe()
    {
        Color[] playerColors = new Color[tiles.Length];
        for (int i = 0; i < tiles.Length; i++)
        {
            playerColors[i] = tiles[i].GetColor();
        }

        // Check if the player's colors match any recipe
        foreach (Recipe recipe in allRecipes)
        {
            bool match = true;
            for (int i = 0; i < recipe.colors.Length; i++)
            {
                if (playerColors[i] != recipe.colors[i])
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                ShowPopup($"Success! You have created a {recipe.recipeName}", recipe.resultSprite);

                if (recipe.recipeName.ToLower() == "carrot")
                {
                    AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc("nanoCarrot");
                }

                if (recipe.recipeName.ToLower() == "wasabi")
                {
                    AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc("jelloWasabi");
                }

                if (recipe.recipeName.ToLower() == "soy sauce")
                {
                    AddToSmallInventory.instance.AddToSmallInventoryAndBigFunc("soySauce");
                }

                return;
            }
        }

        ShowPopup("Incorrect! Try Again", null);
    }

    private void ShowPopup(string message, Sprite image)
    {
        resultPopupPanel.SetActive(true);
        resultText.text = message;
        resultImage.sprite = image;
        resultImage.enabled = image != null;
        //CancelInvoke(nameof(HidePopup));
        //Invoke(nameof(HidePopup), 2.5f);
    }

    //private void HidePopup()
    //{
    //    resultPopupPanel.SetActive(false);
    //}

    //private string ColorName(Color color)
    //{
    //    if (color == Color.green) return "Green";
    //    if (color == Color.white) return "White";
    //    if (color == new Color(1f, 0.5f, 0f)) return "Orange";
    //    if (color == Color.red) return "Red";
    //    if (color == Color.blue) return "Blue";
    //    if (color == Color.yellow) return "Yellow";
    //    return color.ToString();
    //}
}
