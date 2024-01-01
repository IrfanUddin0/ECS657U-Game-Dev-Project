using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : InventoryUIElement
{
    public GameObject CraftingPanel;

    public override void OnInventoryUpdate()
    {
        base.OnInventoryUpdate();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        CraftingManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CraftingManager>();
        foreach(CraftingRecipe i in manager.Craftables)
        {
            GameObject panel = Instantiate(CraftingPanel);
            panel.transform.SetParent(transform, false);
            panel.transform.GetComponentInChildren<CraftingPanelScript>().SetItem(i);
        }

        // if on easy then display non craftable
        if(FindAnyObjectByType<MainPlayerScript>().GetDifficulty()==0)
        {
            foreach (var i in manager.NonCraftable)
            {
                GameObject panel = Instantiate(CraftingPanel);
                panel.transform.SetParent(transform, false);
                panel.transform.GetComponentInChildren<CraftingPanelScript>().craftable = false;
                panel.transform.GetComponentInChildren<CraftingPanelScript>().SetItem(i);
            }
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, (manager.Craftables.Count + manager.NonCraftable.Count) * 96f);
    }
}
