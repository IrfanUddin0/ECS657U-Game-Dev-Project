using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementScript : MonoBehaviour
{
    public float base_walk_speed;
    float walkspeed;
    public float jumpforce = 15F;
    public MovementState movementState=MovementState.Normal;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMovementState(MovementState.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        InputMode mode = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputMode;
        if (mode == InputMode.Playing)
        {
            handleDirectionInput();
            handleJumpInput();
            handleSprintInput();
            handleCrouchInput();

            handleFireInput();
            handleAdsInput();
        }

        sprintTickCheck();
    }

    // HANDLE INPUT FUNCTIONS

    private void handleDirectionInput()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 new_pos = transform.position + (vAxis * (new Vector3(transform.forward.x, 0F, transform.forward.z) * walkspeed * Time.deltaTime));
        new_pos += (hAxis * (transform.right * walkspeed * Time.deltaTime));

        transform.position = new_pos;
    }
    
    private void handleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            onJump();
        }
    }

    private void handleSprintInput()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            onSprint();
        }
    }

    private void handleCrouchInput()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            onCrouch();
        }
    }

    private bool canFire()
    {
        return movementState != MovementState.Sprinting;
    }

    private void handleFireInput()
    {
        EquippableItemEvents wep = GetComponentInChildren<EquippableItemEvents>();
        if (wep == null) { return; }

        if(Input.GetButtonDown("Fire1") && canFire())
        {
            wep.OnFireClicked();
        }

        if(Input.GetButtonUp("Fire1") && canFire())
        {
            wep.OnFireReleased();
        }
    }

    private void handleAdsInput()
    {
        if (Input.GetButtonDown("ADS")){ onADS(); }

        if (Input.GetButtonUp("ADS")){ stopADS(); }
    }

    private void handleReloadInput()
    {
        
    }



    // ON MOVEMENT FUNCTIONS

    private void onJump()
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

    private void onSprint()
    {
        EquippableItemEvents wep = GetComponentInChildren<EquippableItemEvents>();
        if (wep != null && wep.getFireHeld())
        {
            return;
        }
        switch (movementState)
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

        PlayerSurvival surv = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>();
        if (surv.getHunger() <= 0) { return; }

        ChangeMovementState(MovementState.Sprinting);
    }

    private void onCrouch()
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

        GetComponent<CapsuleCollider>().height = 1;
    }

    private void stopCrouch()
    {
        switch (movementState)
        {
            case MovementState.Normal: return;
            case MovementState.NormalADS: return;
            case MovementState.Crouching: ChangeMovementState(MovementState.Normal); break;
            case MovementState.CrouchADS: ChangeMovementState(MovementState.NormalADS); break;
            case MovementState.Sprinting: return;
        }
        GetComponent<CapsuleCollider>().height = 2;
    }

    void onADS()
    {
        switch (movementState)
        {
            case MovementState.Normal: ChangeMovementState(MovementState.NormalADS); break;
            case MovementState.NormalADS: stopADS(); return;
            case MovementState.Crouching: ChangeMovementState(MovementState.CrouchADS); break;
            case MovementState.CrouchADS: stopADS(); return;
            case MovementState.Sprinting: return;
        }

        EquippableItemEvents wep = GetComponentInChildren<EquippableItemEvents>();
        if (wep == null) { return; }
        wep.OnADSClicked();
    }

    public void stopADS()
    {
        switch (movementState)
        {
            case MovementState.Normal: return;
            case MovementState.NormalADS: ChangeMovementState(MovementState.Normal); break;
            case MovementState.Crouching: return;
            case MovementState.CrouchADS: ChangeMovementState(MovementState.Crouching); break;
            case MovementState.Sprinting: return;
        }
        EquippableItemEvents wep = GetComponentInChildren<EquippableItemEvents>();
        if (wep == null) { return; }
        wep.OnADSReleased();
    }

    // OTHER MOVEMENT RELATED FUNCTIONS

    public bool isGrounded()
    {
        float height = transform.GetComponent<Collider>().bounds.size.z + .1f;
        Debug.DrawLine(transform.position, transform.position + (transform.up * -height), Color.red, 10,true);
        if (Physics.Linecast(transform.position, transform.position + (transform.up * -height)))
        {
            return true;
        }
        return false;
    }

    public void jump()
    {
        if (isGrounded())
        {
            Rigidbody capsule = (Rigidbody)transform.GetComponent("Rigidbody");
            capsule.AddForce(transform.up * jumpforce);
        }
    }

    public void ChangeMovementState(MovementState state)
    {
        print("changed movement state to:"+state.ToString());
        switch (state)
        {
            case MovementState.Normal:
                walkspeed = base_walk_speed;
                break;
            case MovementState.NormalADS:
                walkspeed = base_walk_speed * 0.5f;
                break;
            case MovementState.Crouching:
                walkspeed = base_walk_speed * 0.5f;
                break;
            case MovementState.CrouchADS:
                walkspeed = base_walk_speed * 0.33f;
                break;
            case MovementState.Sprinting:
                walkspeed = base_walk_speed * 1.75f;
                break;
        }

        movementState = state;
    }

    public MovementState getMovementState()
    {
        return movementState;
    }

    private void sprintTickCheck()
    {
        if (movementState==MovementState.Sprinting && Input.GetAxis("Vertical")<0.1f ||
            
            movementState == MovementState.Sprinting && 
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>().getHunger() <= 0)
        {
            onSprint();
        }
    }
}
