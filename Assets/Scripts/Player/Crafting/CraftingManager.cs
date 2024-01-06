using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class CraftingManager : InventoryUIElement
{
    public CraftingRecipesCollection recipes;
    public PrefabCollection collection;

    public List<CraftingRecipe> Craftables;
    public List<CraftingRecipe> NonCraftable;
    // Start is called before the first frame update
    void Start()
    {
        Craftables = GetCraftableRecipes();
        NonCraftable = GetNonCraftableRecipes();
    }

    public override void OnInventoryUpdate()
    {
        base.OnInventoryUpdate();
        Craftables = GetCraftableRecipes();
        NonCraftable = GetNonCraftableRecipes();
    }

    List<CraftingRecipe> GetCraftableRecipes()
    {
        List<CraftingRecipe> craftable = new List<CraftingRecipe>();

        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        foreach(CraftingRecipe recipe in recipes.recipes)
        {
            bool can_build = true;
            foreach(RecipeItem item in recipe.ItemsNeeded)
            {
                // check if can build or not
                if (inventory.GetTotalCountOfItem(item.Item) < item.count)
                {
                    can_build = false;
                    break;
                }
            }

            if(can_build) { craftable.Add(recipe); }
        }

        return craftable;
    }

    List<CraftingRecipe> GetNonCraftableRecipes()
    {
        List<CraftingRecipe> noncraftable = new List<CraftingRecipe>();

        foreach(var recepie in recipes.recipes)
        {
            noncraftable.Add(recepie);
        }

        var craftable = GetCraftableRecipes();

        foreach(var recipe in craftable)
        {
            noncraftable.Remove(recipe);
        }

        return noncraftable;
    }

    public void CraftRecipe(CraftingRecipe recipe)
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
     
        Item add = new Item();
        foreach (PrefabMapping i in collection.prefabMappings)
        {
            if(i.valuePrefab == recipe.ItemResult)
            {
                add.PickupPrefab = i.keyPrefab;
                add.EquipPrefab = i.valuePrefab;
                add.itemName = i.PrefabMappingName;
                add.description = i.ItemDescription;
                add.image = i.image;
                add.maxInStack = i.maxInStack;
            }
        }
        inventory.AddItem(add);
        FindAnyObjectByType<PlayerObjectives>().addDataEntry(add.itemName+ "Crafted", "true");
        foreach (RecipeItem item in recipe.ItemsNeeded)
        {
            for (int i = 0; i < item.count; i++)
            {
                inventory.RemoveItem(inventory.GetItemWithPrefab(item.Item),false);
            }
        }
    }

    public PrefabMapping GetMapping(GameObject equippable)
    {
        foreach (PrefabMapping i in collection.prefabMappings)
        {
            if (i.valuePrefab == equippable)
            {
                return i;
            }
        }

        return null;
    }
}
