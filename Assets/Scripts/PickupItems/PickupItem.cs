using System.Collections.Generic;
using UnityEngine;

public class PickupItem : PlayerInteractable
{
    public PrefabCollection prefabCollection;
    // Start is called before the first frame update
    public string MappingName;
    void Start()
    {
        print(prefabCollection.name);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnInteract()
    {
        print("Player Pickup");
        Inventory playerInven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        if(playerInven != null)
        {
            Item item = new Item();
            PrefabMapping mapping = prefabCollection.prefabMappings.Find(x => x.PrefabMappingName == MappingName);
            item.PickupPrefab = mapping.keyPrefab;
            item.EquipPrefab = mapping.valuePrefab;
            item.itemName = mapping.valuePrefab.name;
            item.image = mapping.image;
            item.maxInStack = mapping.maxInStack;
            playerInven.AddItem(item);
            Destroy(gameObject);
        }
    }
}
