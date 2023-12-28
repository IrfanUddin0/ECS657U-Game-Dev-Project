using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/PlacedItem Prefab Collection")]
public class PlacedItemCollection : ScriptableObject
{
    public List<PlacedItemPrefabMapping> prefabMappings = new List<PlacedItemPrefabMapping>();
    public PlacedItemPrefabMapping findWithName(string name)
    {
        foreach (var mapping in prefabMappings)
        {
            if(mapping.PrefabMappingName == name)
                return mapping;
        }
        return null;
    }
}

[System.Serializable]
public class PlacedItemPrefabMapping
{
    public string PrefabMappingName;
    public GameObject Prefab;
}