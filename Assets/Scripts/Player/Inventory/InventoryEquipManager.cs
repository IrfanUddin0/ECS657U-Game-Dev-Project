using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryEquipManager : MonoBehaviour
{
    public int currentEquipIndex = -1;
    private int inventorySlots = 5;

    public AudioClip dropSoundClip;
    public float dropSoundVolume = 1f;

    [SerializeField]
    public InputActionReference Drop;
    [SerializeField]
    public List<InputActionReference> InventorySlots;

    void Start()
    {
    }

    private void OnEnable()
    {
        Drop.action.performed += lambda => OnDrop();

        InventorySlots[0].action.performed += lambda => ChangeEquipIndex(0);
        InventorySlots[1].action.performed += lambda => ChangeEquipIndex(1);
        InventorySlots[2].action.performed += lambda => ChangeEquipIndex(2);
        InventorySlots[3].action.performed += lambda => ChangeEquipIndex(3);
        InventorySlots[4].action.performed += lambda => ChangeEquipIndex(4);
    }

    private void OnDisable()
    {
        Drop.action.performed -= lambda => OnDrop();

        InventorySlots[1].action.performed -= lambda => ChangeEquipIndex(1);
        InventorySlots[2].action.performed -= lambda => ChangeEquipIndex(2);
        InventorySlots[0].action.performed -= lambda => ChangeEquipIndex(0);
        InventorySlots[3].action.performed -= lambda => ChangeEquipIndex(3);
        InventorySlots[4].action.performed -= lambda => ChangeEquipIndex(4);
    }

    private void OnDrop()
    {
        Inventory playerInven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        if(playerInven.RemoveItemByIndex(currentEquipIndex))
            Util.PlayClipAtPoint(dropSoundClip, transform.position, dropSoundVolume);
    }

    void Update()
    {
    }

    public void ChangeEquipIndex(int index)
    {
        print("changing to slot " + index);
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();

        // if out of range OR double clicked then unequip
        if (index >= inventory.getSize() || currentEquipIndex == index || index < 0)
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
        OnEquipChanged();
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
        OnEquipChanged();
    }

    public void OnEquipChanged()
    {
        foreach (InventoryUIElement UIElem in GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<InventoryUIElement>())
        {
            UIElem.OnInventoryUpdate();
        }
    }

    public int getInventorySlots()
    {
        return inventorySlots;
    }
}
