using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    private float timer = 0.0f;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;

    void Update()
    {
        Vector2 input_axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MovementState movementState = transform.parent.transform.parent.GetComponent<MovementScript>().getMovementState();
        float sprint_multiplier = 1f;
        if (movementState==MovementState.Sprinting) { sprint_multiplier = 2f; }

        float elapsed_time = Time.timeSinceLevelLoad;
        Vector3 offset = input_axis.magnitude * bobbingAmount * new Vector3(
            Mathf.Sin(elapsed_time * bobbingSpeed * sprint_multiplier),
            Mathf.Sin(elapsed_time * 2 * bobbingSpeed * sprint_multiplier),
            0);

        Camera.main.transform.localPosition = offset;
    }
}
