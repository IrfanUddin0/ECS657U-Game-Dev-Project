using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 1F;
    public float sensitivityY = 1F;

    public float cameraFieldOfView = 77.0f;

    public float minimumY = -90F;
    public float maximumY = 90F;


    float rotationY = 0F;
    float rotationX = 0F;

    private float fov_transform_velocity;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

        GameObject.FindGameObjectsWithTag("CameraArm")[0].transform.localEulerAngles = new Vector3(-rotationY,0,0);

        interpCameraFov();
    }

    void Start()
    {
        GameObject.FindGameObjectsWithTag("CameraArm")[0].GetComponentInChildren<Camera>().fieldOfView = cameraFieldOfView;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void interpCameraFov()
    {
        Camera activecam = GameObject.FindGameObjectsWithTag("CameraArm")[0].GetComponentInChildren<Camera>();
        MovementState state = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MovementScript>().getMovementState();
        float ads_fov_scale = 1.0f;
        if(GetComponentInChildren<EquippableItemEvents>()!=null)
            ads_fov_scale = GetComponentInChildren<EquippableItemEvents>().ads_fov_scale;
        
        activecam.fieldOfView = Mathf.SmoothDamp(
            activecam.fieldOfView,
            (state == MovementState.NormalADS || state==MovementState.CrouchADS) ? cameraFieldOfView * ads_fov_scale : cameraFieldOfView,
            ref fov_transform_velocity,
            0.25f);
    }
}
