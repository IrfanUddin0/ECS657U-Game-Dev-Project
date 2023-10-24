using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIBottom : MonoBehaviour
{
    public Sprite defaultImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        int equipped = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>().currentEquipIndex;
        List<Item> items = inventory.getItems();

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < Mathf.Min(5, items.Count); i++)
        {
            Item item = items[i];

            GameObject imageObject = new GameObject("ItemImage");
            Image imageComponent = imageObject.AddComponent<Image>();

            if(equipped==i)
                imageComponent.color = Color.white;
            else
                imageComponent.color = Color.gray;

            float currentHeight = GetComponent<RectTransform>().sizeDelta.y;

            imageComponent.rectTransform.sizeDelta = new Vector2(currentHeight, currentHeight);
            imageComponent.rectTransform.SetParent(gameObject.transform, false); // Set parent and don't adjust local position.

            imageComponent.rectTransform.anchorMin = new Vector2(0, 0.5f); // Left-center
            imageComponent.rectTransform.anchorMax = new Vector2(0, 0.5f); // Left-center
            imageComponent.rectTransform.pivot = new Vector2(0, 0.5f);


            if (item.image == null)
                imageComponent.sprite = defaultImage;
            else
                imageComponent.sprite = item.image;

            imageComponent.rectTransform.anchoredPosition = new Vector2(currentHeight * i, 0);
        }
    }
}
