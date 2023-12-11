using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxFogColor : MonoBehaviour
{
    public Gradient fogOverTime;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.fogColor = fogOverTime.Evaluate(transform.localEulerAngles.x / 360f);
    }
}
