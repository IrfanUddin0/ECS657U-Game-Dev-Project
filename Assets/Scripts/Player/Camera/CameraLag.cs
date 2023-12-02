using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLag : MonoBehaviour
{
    public float swayAmountX = 5;
    public float swayAmountY = 5;
    public float smooth = 8;
    public float rotationAmmountDegH = 7;
    public float rotationAmmountDegV = 3;

    private void Update()
    {
        InputMode mode = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputMode;
        if (mode == InputMode.Playing)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * swayAmountX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * swayAmountY;

            float movement_Multiplier = 1.0f;
            MovementScript mov = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MovementScript>();
            if (mov.getMovementState() == MovementState.NormalADS || mov.getMovementState() == MovementState.CrouchADS)
                movement_Multiplier = 0.1f;

            float directionRotationAmmountHorizontal = Mathf.Clamp(Input.GetAxis("Horizontal") * rotationAmmountDegH * -1, -rotationAmmountDegH, rotationAmmountDegH / 2);
            float directionRotationAmmountVerical = Input.GetAxis("Vertical") * rotationAmmountDegV * movement_Multiplier;

            Quaternion rotationX = Quaternion.AngleAxis(-mouseY + directionRotationAmmountVerical, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
            Quaternion rotationZ = Quaternion.AngleAxis(directionRotationAmmountHorizontal, Vector3.forward);

            Quaternion targetRot = rotationX * rotationY * rotationZ;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smooth * Time.deltaTime);
        }
    }
}
