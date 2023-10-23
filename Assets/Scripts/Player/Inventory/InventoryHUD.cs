using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{
    public GameObject inventoryHUDPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventoryView();

    }

    private void UpdateInventoryView()
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
            panel.transform.localPosition = new Vector3(0, i*-64, 0);
        }
    }
}
