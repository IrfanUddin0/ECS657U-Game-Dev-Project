using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = transform.rotation;
        transform.rotation = new Quaternion(rot.x, transform.Find("Camera").transform.rotation.y, rot.z, rot.w);

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 hMove = hAxis * transform.right;
        Vector3 vMove = vAxis * transform.up;
        Vector3 Movement = (hMove + vMove).normalized * 1;
        print(Movement);
        transform.position += Movement;
    }
}
