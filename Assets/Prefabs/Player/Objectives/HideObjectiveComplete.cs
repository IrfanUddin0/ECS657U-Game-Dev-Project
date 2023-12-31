using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjectiveComplete : MonoBehaviour
{
    private float started;
    private float hideAfter = 3f;
    void Start()
    {
        started = Time.timeSinceLevelLoad;
    }

    private void FixedUpdate()
    {
        if(Time.timeSinceLevelLoad - started >= hideAfter)
        {
            Destroy(gameObject);
        }
    }
}
