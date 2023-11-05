using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIElement : MonoBehaviour
{
    public virtual void OnInventoryUpdate(){}

    private void OnEnable()
    {
        OnInventoryUpdate();
    }
}
