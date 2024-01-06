using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : PlayerInteractable
{
    public AudioClip BedNextDaySound;
    public float volume;
    Light mainLightRef;
    // Start is called before the first frame update
    void Start()
    {
        mainLightRef = FindAnyObjectByType<SkyboxFogColor>().GetComponentInChildren<Light>();
    }

    public override void OnInteract()
    {
        base.OnInteract();

        // set spawn point 
        var player = FindAnyObjectByType<MainPlayerScript>();
        player.spawnTransform = new SpawnTransform(player.transform.position, player.transform.rotation);

        // if night and light anim is real, reset sun anim position
        if (isNight()
            && mainLightRef.GetComponent<Animator>() != null)
        {
            mainLightRef.GetComponent<Animator>().SetFloat("AnimPos", 0.0f);
            Util.PlayClipAtPoint(BedNextDaySound, transform.position, volume);
        }
    }

    private bool isNight()
    {
        return (mainLightRef != null
            && mainLightRef.transform.localEulerAngles.x / 360f >= 0.5f);
    }
}
