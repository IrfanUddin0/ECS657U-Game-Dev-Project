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
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();

        icon.sprite = item.image;
        texts[0].text = item.itemName;
        texts[1].text = ""+item.count;
        texts[2].text = item.description;

        // if equipped then hide equip button
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        InventoryEquipManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
        if (inven.FindIndexByItem(item) < manager.getInventorySlots())
        {
            buttons[2].colors = new ColorBlock();
            buttons[2].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }
            
        else
            buttons[2].onClick.AddListener(EquipButtonPressed);

        buttons[0].onClick.AddListener(DropAllPressed);
        buttons[1].onClick.AddListener(DropOnePressed);
    }

    public static void test()
    {
        print("aaa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EquipButtonPressed()
    {
        print("called EquipButtonPressed");
        InventoryEquipManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();

        // if already equipped then do nothing
        if (inven.FindIndexByItem(item) < manager.getInventorySlots())
            return;

        // if currently not equipped anything then swap the first item
        if(manager.currentEquipIndex == -1)
        {
            inven.SwapItemPositions(inven.getItemAtIndex(0), item);
            manager.ChangeEquipIndex(0);
        }
        // else swap the currently eqipped item
        else
        {
            int index = manager.currentEquipIndex;
            inven.SwapItemPositions(inven.getItemAtIndex(index), item);
            manager.ChangeEquipIndex(-1);
            manager.ChangeEquipIndex(index);
        }
    }

    void DropOnePressed()
    {
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        inven.RemoveItem(item);
    }

    void DropAllPressed()
    {
        Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        int count = item.count;
        for (int i = 0; i < count; i++)
        {
            inven.RemoveItem(item);
        }
    }
}
