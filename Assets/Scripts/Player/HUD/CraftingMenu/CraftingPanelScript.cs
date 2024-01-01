using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanelScript : MonoBehaviour
{
    CraftingRecipe Recipe;
    public AudioClip craftSound;
    public bool craftable = true;
    public void SetItem(CraftingRecipe item)
    {
        CraftingManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CraftingManager>();
        Recipe = item;
        GetComponentsInChildren<Image>()[1].sprite = manager.GetMapping(item.ItemResult).image;

        HorizontalLayoutGroup spacer = GetComponentInChildren<HorizontalLayoutGroup>();
        foreach(RecipeItem r in item.ItemsNeeded)
        {
            GameObject img = new GameObject("Image");
            img.AddComponent<RectTransform>();
            img.GetComponent<RectTransform>().sizeDelta = new Vector3(64, 64);
            img.AddComponent<Image>();
            img.GetComponent<Image>().sprite = manager.GetMapping(r.Item).image;
            img.transform.SetParent(spacer.transform, false);

            GameObject txt = new GameObject("Text");
            txt.AddComponent<RectTransform>();
            img.GetComponent<RectTransform>().sizeDelta = new Vector3(64, 64);
            txt.AddComponent<TextMeshProUGUI>();
            txt.GetComponent<TextMeshProUGUI>().text = r.count.ToString();
            txt.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            txt.transform.SetParent(img.transform, false);
        }
        if (craftable)
            GetComponentInChildren<Button>().onClick.AddListener(OnCraftClicked);
        else
        {
            GetComponentInChildren<Button>().gameObject.SetActive(false);
            GetComponentInChildren<Image>().color = new Color(1f,1f,1f,.4f);
        }
            
    }

    void OnCraftClicked()
    {
        Util.PlayClipAtPoint(craftSound, GameObject.FindGameObjectWithTag("Player").transform.position, 1f);
        CraftingManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CraftingManager>();
        manager.CraftRecipe(Recipe);
    }
}
