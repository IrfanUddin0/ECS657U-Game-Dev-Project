using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Prefab Collection")]
public class PrefabCollection : ScriptableObject
{
    public List<PrefabMapping> prefabMappings = new List<PrefabMapping>();
}

[System.Serializable]
public class PrefabMapping
{
    public string PrefabMappingName;
    public string ItemDescription;
    public GameObject keyPrefab;
    public GameObject valuePrefab;
    public Sprite image;
    public int maxInStack;
}