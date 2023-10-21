using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> itemList = new List<Item>();

    // Method to add an item to the inventory.
    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    // Method to remove an item from the inventory.
    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
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

    // Method to spawn an instance of the item in the world.
    private void SpawnItemInWorld(Item item)
    {
        Transform spawnPoint = GameObject.FindGameObjectWithTag("CameraArm").transform;
        if (spawnPoint != null && item != null)
        {
            Instantiate(item.PickupPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void Update()
    {
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public GameObject PickupPrefab;
    public GameObject EquipPrefab;
}