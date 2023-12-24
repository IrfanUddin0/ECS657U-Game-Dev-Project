using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : PlayerInteractable
{
    Light mainLightRef;
    // Start is called before the first frame update
    void Start()
    {
        mainLightRef = FindAnyObjectByType<SkyboxFogColor>().GetComponentInChildren<Light>();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if(isNight())
        {
            if(mainLightRef.GetComponent<Animator>() != null)
                mainLightRef.GetComponent<Animator>().SetFloat("AnimPos", 0.0f);
        }
    }

    private bool isNight()
    {
        return (mainLightRef != null
            && mainLightRef.transform.localEulerAngles.x / 360f >= 0.5f);
    }
}
