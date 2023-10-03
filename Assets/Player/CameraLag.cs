using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLag : MonoBehaviour
{
    private float swayAmountX = 5;
    private float swayAmountY = 2;
    private float smooth = 8;

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayAmountX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayAmountY;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRot = rotationX * rotationY;

        print(targetRot);   

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smooth * Time.deltaTime);
    }
}
