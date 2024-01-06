using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
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

        OnInventoryUpdated();
    }

    // Method to remove an item from the inventory.
    public void RemoveItem(Item item, bool spawnitem = true)
    {
        if (itemList.Contains(item))
        {
            InventoryEquipManager manager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
            if (manager.currentEquipIndex == FindIndexByItem(item))
                manager.UnEquip();

            if (FindIndexByItem(item) < manager.currentEquipIndex)
                manager.currentEquipIndex -= 1;

            if(spawnitem)
                SpawnItemInWorld(item);

            if(item.count<=1)
                itemList.Remove(item);
            else
            {
                item.count--;
            }
        }

        OnInventoryUpdated();
    }

    public bool RemoveItemByIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            Item item = itemList[index];
            RemoveItem(item);
            return true;
        }
        return false;
    }

    public void RemoveEveryItem()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            RemoveItem(itemList[i]);
            i--;
        }
    }

    public void SwapItemPositions(Item item1,  Item item2)
    {
        int index1 = itemList.IndexOf(item1);
        int index2 = itemList.IndexOf(item2);
        itemList[index1] = item2;
        itemList[index2] = item1;
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

    public void OnInventoryUpdated()
    {
        foreach(InventoryUIElement UIElem in GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<InventoryUIElement>())
        {
            UIElem.OnInventoryUpdate();
        }
    }

    public int GetTotalCountOfItem(GameObject item_looking)
    {
        int totalCount = 0;
        foreach(Item item in itemList)
        {
            if(item.EquipPrefab == item_looking) totalCount+= item.count;
        }
        return totalCount;
    }

    public Item GetItemWithPrefab(GameObject prefab)
    {
        foreach (Item item in itemList)
        {
            if(item.EquipPrefab == prefab) return item;
        }
        return null;
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public GameObject PickupPrefab;
    public GameObject EquipPrefab;
    public Sprite image;
    public int maxInStack;
    public int count;
}