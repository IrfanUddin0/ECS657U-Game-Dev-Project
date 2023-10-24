using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipManager : MonoBehaviour
{
    public int currentEquipIndex = -1;
    private int inventorySlots = 5;

    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < inventorySlots; i++)
        {
            if(Input.GetButtonDown("Slot"+(i+1)))
            {
                ChangeEquipIndex(i);
            }
        }
    }

    public void ChangeEquipIndex(int index)
    {
        print("changing to slot " + index);
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();

        // if out of range OR double clicked then unequip
        if (index >= inventory.getSize() || currentEquipIndex == index)
        {
            UnEquip();
            return;
        }
        UnEquip();

        GameObject spawner = GameObject.FindGameObjectWithTag("EquipSpawner");
        GameObject spawnedEquippable = Instantiate(inventory.getItemAtIndex(index).EquipPrefab);
        spawnedEquippable.transform.SetParent(spawner.transform);
        spawnedEquippable.transform.position = spawner.transform.position;
        spawnedEquippable.transform.rotation = spawner.transform.rotation;
        currentEquipIndex = index;
    }

    public void UnEquip()
    {
        // set equip index to none
        currentEquipIndex = -1;
        // remove child elems of equip
        GameObject spawner = GameObject.FindGameObjectWithTag("EquipSpawner");
        foreach (Transform child in spawner.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        MovementScript player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MovementScript>();
        player.stopADS();
    }
}
