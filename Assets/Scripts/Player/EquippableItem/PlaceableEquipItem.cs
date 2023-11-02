using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlaceableEquipItem : EquippableItemEvents
{
    public GameObject ItemToPlace;
    public float PlaceRange = 5f;

    public Material PlaceMaterial;

    private GameObject PreviewItemPlace;
    private float y_rot = 0;

    public override void Start()
    {
        base.Start();
        PreviewItemPlace = Instantiate(ItemToPlace);
        foreach(MeshRenderer i in PreviewItemPlace.GetComponentsInChildren<MeshRenderer>())
        {
            i.material = PlaceMaterial;
        }

        foreach(Collider i in PreviewItemPlace.GetComponentsInChildren<Collider>())
        {
            i.enabled = false;
        }
    }

    private void OnDestroy()
    {
        Destroy(PreviewItemPlace);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Rotate90"))
            y_rot += 90f;

        GameObject cam = GameObject.FindGameObjectsWithTag("CameraArm")[0];
        RaycastHit hit;
        Physics.Linecast(cam.transform.position + 0.2f * cam.transform.forward, PlaceRange * cam.transform.forward + cam.transform.position, out hit);


        PreviewItemPlace.transform.position = hit.point;
        PreviewItemPlace.transform.rotation = Quaternion.Euler(0f, y_rot, 0f);
    }

    public override void OnFireClicked()
    {
        base.OnFireClicked();
        OnPlace();
    }

    protected override void OnADSHeld()
    {
        base.OnADSHeld();

        float rotationSpeed = 1.0f;

        float mouseX = Input.GetAxis("Mouse X"); // Get the mouse input on the X-axis
        y_rot += mouseX * rotationSpeed; // Update the rotation angle
    }

    protected void OnPlace()
    {

        GameObject cam = GameObject.FindGameObjectsWithTag("CameraArm")[0];
        RaycastHit hit;
        Physics.Linecast(cam.transform.position + 0.2f * cam.transform.forward, PlaceRange * cam.transform.forward + cam.transform.position, out hit);

        if(hit.collider != null)
        {
            GameObject final_placed = Instantiate(ItemToPlace);
            final_placed.transform.position = hit.point;
            final_placed.transform.rotation = Quaternion.Euler(0f, y_rot, 0f);

            Inventory inven = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
            InventoryEquipManager managerr = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryEquipManager>();
            inven.RemoveItem(inven.getItemAtIndex(managerr.currentEquipIndex), false);
        }
    }
}
