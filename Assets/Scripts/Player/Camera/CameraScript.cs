using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 0.5F;
    public float sensitivityY = 0.5F;

    public float cameraFieldOfView = 77.0f;

    public float minimumY = -90F;
    public float maximumY = 90F;


    public float rotationY = 0F;
    public float rotationX = 0F;

    private float fov_transform_velocity;
    private List<CameraShake> activeCameraShakes = new List<CameraShake>();

    [SerializeField]
    public InputActionReference Mouse;
    void Start()
    {
        GameObject.FindGameObjectsWithTag("CameraArm")[0].GetComponentInChildren<Camera>().fieldOfView = cameraFieldOfView;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
        var loadedsens = PlayerPrefs.GetFloat("MouseSensitivity");
        if (PlayerPrefs.HasKey("MouseSensitivity") && loadedsens>=0f && loadedsens<=1f)
        {
            sensitivityX = PlayerPrefs.GetFloat("MouseSensitivity");
            sensitivityY = PlayerPrefs.GetFloat("MouseSensitivity");
            print(PlayerPrefs.GetFloat("MouseSensitivity"));
        }
        else
        {
            sensitivityX = 0.05F;
            sensitivityY = 0.05F;
        }
            
    }

    void Update()
    {
        InputMode mode = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputMode;
        if(mode==InputMode.Playing)
        {
            Vector2 mouserot = Mouse.action.ReadValue<Vector2>();
            rotationY += mouserot.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            rotationX += mouserot.x * sensitivityX;

            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            transform.Rotate(0, mouserot.x * sensitivityX, 0);

            GameObject.FindGameObjectsWithTag("CameraArm")[0].transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }

        interpCameraFov();
        PlayCameraShake();
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

    public void AddCameraShakeToPlay(CameraShake shake)
    {
        activeCameraShakes.Add(shake);
    }

    void PlayCameraShake()
    {
        for (int i = 0;i< activeCameraShakes.Count;i++)
        {
            CameraShake cameraShake = activeCameraShakes[i];
            cameraShake.PlayShakeUpdate();

            if (cameraShake.duration == 0)
                activeCameraShakes.Remove(cameraShake);
        }
    }
}

public class CameraShake
{
    public Transform camTransform;
    public float duration;
    public float shakeAmount;
    public float decreaseFactor;
    public float smoothness;

    protected Vector3 originalPos;
    protected Quaternion originalRot;
    private Vector3 currentSmoothVelocity;

    public CameraShake(Transform camT, float p_shakeDuration, float p_shakeAmount, float p_decreaseFactor, float p_smoothness)
    {
        camTransform = camT;
        originalPos = camTransform.localPosition;
        originalRot = camTransform.localRotation;
        duration = p_shakeDuration;
        shakeAmount = p_shakeAmount;
        decreaseFactor = p_decreaseFactor;
        smoothness = p_smoothness;
    }

    public virtual void PlayShakeUpdate()
    {
        if (duration > 0)
        {
            float jumpAngle = shakeAmount * Mathf.Sin(2 * Mathf.PI * 2f * duration);
            camTransform.localRotation = Quaternion.Euler(jumpAngle, 0, 0);

            duration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            duration = 0f;
            camTransform.localPosition = originalPos;
            camTransform.localRotation = originalRot;
        }
    }
}



public class CameraShakeRandom : CameraShake
{
    private Vector3 currentSmoothVelocity;

    public CameraShakeRandom(Transform camT, float p_shakeDuration, float p_shakeAmount, float p_decreaseFactor, float p_smoothness)
        : base(camT, p_shakeDuration, p_shakeAmount, p_decreaseFactor, p_smoothness)
    {
    }

    public override void PlayShakeUpdate()
    {
        if (duration > 0)
        {
            camTransform.localPosition = Vector3.SmoothDamp(
                camTransform.localPosition,
                originalPos + Random.insideUnitSphere * shakeAmount,
                ref currentSmoothVelocity,
                smoothness);

            duration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            duration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}