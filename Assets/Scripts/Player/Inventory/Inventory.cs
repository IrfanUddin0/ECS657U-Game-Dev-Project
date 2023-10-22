using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> itemList = new List<Item>();

    // Method to add an item to the inventory.
    public void AddItem(Item item)
    {
        itemList.Add(item);

        // if this is the first item added, equip it
        if(itemList.Count == 1)
        {
            InventoryEquipManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
            manager.ChangeEquipIndex(0);
        }
    }

    // Method to remove an item from the inventory.
    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
            InventoryEquipManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
            if (manager.currentEquipIndex == FindIndexByItem(item))
                manager.UnEquip();

            if (FindIndexByItem(item) < manager.currentEquipIndex)
                manager.currentEquipIndex -= 1;
            itemList.Remove(item);
            SpawnItemInWorld(item);

        }
    }

    public void RemoveItemByIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            Item item = itemList[index];
            RemoveItem(item);
        }
    }

    public ref List<Item> getItems()
    {
        return ref itemList;
    }

    public int getSize()
    {
        return itemList.Count;
    }

    public int FindIndexByItem(Item item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == item) { return i; }
        }
        return -1;
    }

    public Item getItemAtIndex(int index)
    {
        return itemList[index];
    }

    // Method to spawn an instance of the item in the world.
    private void SpawnItemInWorld(Item item)
    {
        Transform spawnPoint = GameObject.FindGameObjectWithTag("CameraArm").transform;
        if (spawnPoint != null && item != null)
        {
            Instantiate(item.PickupPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public GameObject PickupPrefab;
    public GameObject EquipPrefab;
    public Sprite image;
}