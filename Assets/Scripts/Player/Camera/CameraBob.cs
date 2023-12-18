using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBob : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;

    [SerializeField]
    public InputActionReference Movement;
    void Update()
    {
        Vector2 input_axis = Movement.action.ReadValue<Vector2>();
        MovementState movementState = transform.parent.transform.parent.GetComponent<MovementScript>().getMovementState();
        float sprint_multiplier = 1f;
        if (movementState==MovementState.Sprinting) { sprint_multiplier = 2f; }

        float ads_multiplier = 1f;
        if (movementState == MovementState.NormalADS || movementState == MovementState.CrouchADS)
            ads_multiplier = 0.1f;

        float elapsed_time = Time.timeSinceLevelLoad;
        Vector3 offset = input_axis.magnitude * (ads_multiplier * bobbingAmount) * new Vector3(
            Mathf.Sin(elapsed_time * bobbingSpeed * sprint_multiplier),
            Mathf.Sin(elapsed_time * 2 * bobbingSpeed * sprint_multiplier),
            0);

        Camera.main.transform.localPosition = offset;
    }
}
