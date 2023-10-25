using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBottomIcon : MonoBehaviour
{
    public Item item;
    public void SetItem(Item item, bool equipped, Sprite defaultImage)
    {
        this.item = item;
        Image icon = transform.gameObject.GetComponentsInChildren<Image>()[1];
        Image border = transform.gameObject.GetComponentsInChildren<Image>()[0];
        TextMeshProUGUI text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        icon.sprite = item.image;
        text.text = "" + item.count;

        if(!equipped)
        {
            border.color = Color.clear;
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
