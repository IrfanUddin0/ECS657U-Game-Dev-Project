using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementScript : MonoBehaviour
{
    float walkspeed = 5F;
    public float jumpforce = 15F;
    public MovementState movementState=MovementState.Normal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleDirectionInput();
        handleJumpInput();
        handleSprintInput();
        handleCrouchInput();

        sprintTickCheck();
    }

    // HANDLE INPUT FUNCTIONS

    void handleDirectionInput()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.position = transform.position + (vAxis * (new Vector3(transform.forward.x, 0F, transform.forward.z) * walkspeed * Time.deltaTime));
        transform.position = transform.position + (hAxis * (transform.right * walkspeed * Time.deltaTime));
    }
    
    void handleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            onJump();
        }
    }

    void handleSprintInput()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            onSprint();
        }
    }

    void handleCrouchInput()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            onCrouch();
        }
    }

    // ON MOVEMENT FUNCTIONS

    void onJump()
    {
        switch(movementState)
        {
            case MovementState.Crouching:
                stopCrouch();
                break;
            case MovementState.CrouchADS:
                stopCrouch();
                break;
        }

        jump();
    }

    void onSprint()
    {
        switch(movementState)
        {
            case MovementState.NormalADS:
                return;
            case MovementState.Crouching:
                stopCrouch();
                break;
            case MovementState.CrouchADS:
                return;
            case MovementState.Sprinting:
                ChangeMovementState(MovementState.Normal);
                return;
        }

        ChangeMovementState(MovementState.Sprinting);
    }

    void onCrouch()
    {
        switch(movementState)
        {
            case MovementState.Normal:
                ChangeMovementState(MovementState.Crouching); break;
            case MovementState.NormalADS:
                ChangeMovementState(MovementState.CrouchADS); break;
            case MovementState.Crouching:
                stopCrouch(); return;
            case MovementState.CrouchADS:
                stopCrouch(); return;
            case MovementState.Sprinting: return;
        }

        transform.GetChild(0).GetComponent<CapsuleCollider>().height = 1;
    }

    void stopCrouch()
    {
        switch (movementState)
        {
            case MovementState.Normal: return;
            case MovementState.NormalADS: return;
            case MovementState.Crouching: ChangeMovementState(MovementState.Normal); break;
            case MovementState.CrouchADS: ChangeMovementState(MovementState.NormalADS); break;
            case MovementState.Sprinting: return;
        }
        transform.GetChild(0).GetComponent<CapsuleCollider>().height = 2;
    }

    void onADS()
    {
        switch (movementState)
        {

        }
    }

    void stopADS()
    {

    }

    // OTHER MOVEMENT RELATED FUNCTIONS

    bool isGrounded()
    {
        float height = transform.GetChild(0).GetComponent<Collider>().bounds.size.z + .1f;
        Debug.DrawLine(transform.position, transform.position + (transform.up * -height), Color.red, 10,true);
        if (Physics.Linecast(transform.position, transform.position + (transform.up * -height)))
        {
            return true;
        }
        return false;
    }

    void jump()
    {
        if (isGrounded())
        {
            Rigidbody capsule = (Rigidbody)transform.GetComponent("Rigidbody");
            capsule.AddForce(transform.up * jumpforce);
        }
    }

    void ChangeMovementState(MovementState state)
    {
        print("changed movement state to:"+state.ToString());
        switch (state)
        {
            case MovementState.Normal:
                walkspeed = 5F;
                break;
            case MovementState.NormalADS:
                walkspeed = 2.5F;
                break;
            case MovementState.Crouching:
                walkspeed = 2.5F;
                break;
            case MovementState.CrouchADS:
                walkspeed = 2F;
                break;
            case MovementState.Sprinting:
                walkspeed = 8F;
                break;
        }

        movementState = state;
    }

    void sprintTickCheck()
    {
        if(movementState==MovementState.Sprinting && Input.GetAxis("Vertical")<1)
        {
            onSprint();
        }
    }
}
