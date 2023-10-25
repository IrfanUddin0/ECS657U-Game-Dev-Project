using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIBottom : MonoBehaviour
{
    public Sprite defaultImage;
    public GameObject InventoryBottomPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        int equipped = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>().currentEquipIndex;
        List<Item> items = inventory.getItems();

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < Mathf.Min(5, items.Count); i++)
        {
            Item item = items[i];
            GameObject icon = Instantiate(InventoryBottomPrefab, transform);
            icon.GetComponentInChildren<InventoryBottomIcon>().SetItem(item, equipped == i, defaultImage);
        }
    }
}
