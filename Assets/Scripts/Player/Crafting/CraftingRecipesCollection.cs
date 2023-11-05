using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Recipes")]
public class CraftingRecipesCollection : ScriptableObject
{
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();
}

[System.Serializable]
public class CraftingRecipe
{
    public List<RecipeItem> ItemsNeeded;
    public GameObject ItemResult;
}

[System.Serializable]
public struct RecipeItem
{
    public GameObject Item;
    public int count;
}