using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : InventoryUIElement
{
    public GameObject inventoryHUDPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnInventoryUpdate()
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        List<Item> items = inventory.getItems();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < items.Count; i++)
        {
            GameObject panel = Instantiate(inventoryHUDPanel);
            panel.transform.SetParent(transform, false);
            //panel.transform.localPosition = new Vector3(0, i*-64, 0);

            panel.transform.GetComponentInChildren<InventoryPanelScript>().SetItem(items[i]);
        }

        // resize scrollbox to fit all items
        GetComponent<RectTransform>().sizeDelta = new Vector2 (635.8157f, items.Count * 64f);
    }
}
