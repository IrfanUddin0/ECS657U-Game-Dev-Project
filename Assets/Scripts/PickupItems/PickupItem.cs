using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
public class PickupItem : PlayerInteractable
{
    public PrefabCollection prefabCollection;
    // Start is called before the first frame update
    public string MappingName;
    void Start()
    {
        foreach(MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
        {
            child.gameObject.AddComponent<cakeslice.Outline>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnInteract()
    {
        base.OnInteract();
        print("Player Pickup");
        Inventory playerInven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        if(playerInven != null)
        {
            Item item = new Item();
            PrefabMapping mapping = prefabCollection.prefabMappings.Find(x => x.PrefabMappingName == MappingName);
            item.PickupPrefab = mapping.keyPrefab;
            item.EquipPrefab = mapping.valuePrefab;
            item.itemName = mapping.PrefabMappingName;
            item.description = mapping.ItemDescription;
            item.image = mapping.image;
            item.maxInStack = mapping.maxInStack;
            playerInven.AddItem(item);
            Destroy(gameObject);
        }
    }
}
