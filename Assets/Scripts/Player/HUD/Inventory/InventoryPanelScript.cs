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
        TextMeshProUGUI name = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();

        icon.sprite = item.image;
        name.text = item.itemName;
        //buttons[0].onClick.AddListener();
        buttons[1].onClick.AddListener(
            () =>
            {
                print("clicked");
                Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
                inventory.RemoveItem(item);
            });
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
