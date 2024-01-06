using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class MovementScript : MonoBehaviour
{
    public float base_walk_speed;
    float walkspeed;
    public float jumpforce = 15F;
    public MovementState movementState=MovementState.Normal;

    [SerializeField]
    public InputActionReference Movement, Jump, Sprint, Crouch, Fire, ADS;

    private bool firing = false;
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

            handleFireInput();
            handleAdsInput();
        }

        sprintTickCheck();
    }

    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (isGrounded())
            rb.velocity = Vector3.zero;
    }

    // HANDLE INPUT FUNCTIONS

    private void OnEnable()
    {
        Jump.action.performed += action => onJump();
        Sprint.action.performed += action => onSprint();
        Crouch.action.performed += action => onCrouch();
    }

    private void OnDisable()
    {
        Jump.action.performed -= action => onJump();
        Sprint.action.performed -= action => onSprint();
        Crouch.action.performed -= action => onCrouch();
    }

    private void handleDirectionInput()
    {
        Vector2 movementVal = Movement.action.ReadValue<Vector2>();
        float hAxis = movementVal.x;
        float vAxis = movementVal.y;

        Vector3 new_pos = transform.position + (vAxis * (new Vector3(transform.forward.x, 0F, transform.forward.z) * walkspeed * Time.deltaTime));
        new_pos += (hAxis * (transform.right * walkspeed * Time.deltaTime));

        transform.position = new_pos;
    }

    private bool canFire()
    {
        return movementState != MovementState.Sprinting;
    }

    private void handleFireInput()
    {
        EquippableItemEvents wep = GetComponentInChildren<EquippableItemEvents>();
        if (wep == null) { return; }

        if(!firing && Fire.action.IsPressed() && canFire())
        {
            firing = true;
            wep.OnFireClicked();
        }

        if(firing && !Fire.action.IsPressed() && canFire())
        {
            firing = false;
            wep.OnFireReleased();
        }
    }

    private void handleAdsInput()
    {
        if (ADS.action.IsPressed() && movementState != MovementState.NormalADS && movementState != MovementState.CrouchADS)
        {
            onADS();
        }
        else if (!ADS.action.IsPressed() && (movementState ==MovementState.NormalADS || movementState == MovementState.CrouchADS))
        {
            stopADS();
        }
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
        float height = transform.GetComponent<CapsuleCollider>().height / 2f + .1f;
        bool cast = Physics.Linecast(transform.position, transform.position + (transform.up * -height));
        Debug.DrawLine(transform.position, transform.position + (transform.up * -height), cast? Color.red : Color.green, 10,true);
        return cast;
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
        // print("changed movement state to:"+state.ToString());
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

    public float GetWalkSpeed()
    {
        return walkspeed;
    }

    public MovementState getMovementState()
    {
        return movementState;
    }

    public float GetInputVelocity()
    {
        return Vector2.Distance(Vector2.zero, Movement.action.ReadValue<Vector2>());
    }

    private void sprintTickCheck()
    {
        if (movementState==MovementState.Sprinting && Movement.action.ReadValue<Vector2>().y<0.1f ||
            
            movementState == MovementState.Sprinting && 
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSurvival>().getHunger() <= 0)
        {
            onSprint();
        }
    }
}
