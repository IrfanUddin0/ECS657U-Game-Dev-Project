using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> itemList = new List<Item>();

    // Method to add an item to the inventory.
    public void AddItem(Item item)
    {
        if(findLastItemAlreadyInInventory(item)!=null && item.maxInStack > findLastItemAlreadyInInventory(item).count)
        {
            findLastItemAlreadyInInventory(item).count++;
        }
        else
        {
            item.count = 1;
            itemList.Add(item);
        }

        // if this is the first item added, equip it
        if(itemList.Count == 1 && item.count==1)
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
            SpawnItemInWorld(item);

            print("REMOVING ITEM COUNT IS:" + item.count);
            if(item.count<=1)
                itemList.Remove(item);
            else
            {
                item.count--;
            }
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

    private Item findLastItemAlreadyInInventory(Item item)
    {
        Item outItem = null;
        foreach (Item invenItem in itemList)
        {
            if(invenItem.EquipPrefab == item.EquipPrefab)
                outItem = invenItem;
        }
        return outItem;
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public GameObject PickupPrefab;
    public GameObject EquipPrefab;
    public Sprite image;
    public int maxInStack;
    public int count;
}