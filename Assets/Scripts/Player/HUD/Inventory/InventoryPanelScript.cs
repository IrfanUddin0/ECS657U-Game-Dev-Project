using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelScript : MonoBehaviour
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        Image icon = transform.GetChild(1).gameObject.GetComponentInChildren<Image>();
        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        icon.sprite = item.image;
        texts[0].text = item.itemName;
        texts[1].text = ""+item.count;
    }

    public static void test()
    {
        print("aaa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
