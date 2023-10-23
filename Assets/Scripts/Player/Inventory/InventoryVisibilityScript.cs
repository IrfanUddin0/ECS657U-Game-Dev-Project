using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisibilityScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("InventoryKey"))
            GetComponent<RectTransform>().localScale = (GetComponent<RectTransform>().localScale==Vector3.zero)?new Vector3(1, 1, 1) : Vector3.zero;
    }
}
