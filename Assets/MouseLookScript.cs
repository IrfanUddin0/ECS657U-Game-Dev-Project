using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 1F;
    public float sensitivityY = 1F;

    public float minimumY = -90F;
    public float maximumY = 90F;

    float rotationY = 0F;
    float rotationX = 0F;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

        Camera.main.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
