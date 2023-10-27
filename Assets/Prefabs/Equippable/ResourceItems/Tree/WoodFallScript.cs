using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere,ForceMode.Impulse);
    }
}
