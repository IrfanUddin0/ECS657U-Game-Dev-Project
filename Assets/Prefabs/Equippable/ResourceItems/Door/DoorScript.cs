using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : PlayerInteractable
{
    bool open = false;
    public override void OnInteract()
    {
        base.OnInteract();
        transform.rotation *= Quaternion.AngleAxis(open? -90f : 90f, Vector3.up);
        open = !open;
    }
}
