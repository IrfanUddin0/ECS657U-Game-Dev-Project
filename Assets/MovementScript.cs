using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementScript : MonoBehaviour
{
    public float walkspeed = 5F;
    public float jumpforce = 15F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleDirectionInput();
        handleJumpInput();
    }

    void handleDirectionInput()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.position = transform.position + (vAxis * (new Vector3(transform.forward.x, 0F, transform.forward.z) * walkspeed * Time.deltaTime));
        transform.position = transform.position + (hAxis * (transform.right * walkspeed * Time.deltaTime));
    }
    
    void handleJumpInput()
    {
        if (Input.GetAxis("Jump")>0)
        {
            jump();
        }
    }

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
}
