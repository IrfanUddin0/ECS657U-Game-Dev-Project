using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class PrefabMapping
{
    public string PrefabMappingName;
    public GameObject keyPrefab;
    public GameObject valuePrefab;
}

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "Custom/Prefab Collection")]
public class PrefabCollection : ScriptableObject
{
    public List<PrefabMapping> prefabMappings = new List<PrefabMapping>();
}

public class PickupItem : PlayerInteractable
{
    [SerializeField] public PrefabCollection prefabCollection;
    // Start is called before the first frame update
    public string MappingName;
    void Start()
    {
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
            print(gameObject.IsPrefabInstance());
            Item item = new Item();
            PrefabMapping mapping = prefabCollection.prefabMappings.Find(x => x.PrefabMappingName == MappingName);
            item.PickupPrefab = mapping.keyPrefab;
            item.EquipPrefab = mapping.valuePrefab;
            item.itemName = mapping.valuePrefab.name;
            playerInven.AddItem(item);
            Destroy(gameObject);
        }
    }
}
