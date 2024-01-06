using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FootstepSounds : MonoBehaviour
{
    public List<AudioClip> sounds;

    MovementScript movementRef;
    private float lastPlayed;
    // Start is called before the first frame update
    void Start()
    {
        movementRef = FindAnyObjectByType<MovementScript>();
        lastPlayed = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad - lastPlayed > 2f / movementRef.GetWalkSpeed()
            && movementRef.GetInputVelocity() >= 0.1f
            && movementRef.isGrounded())
        {
            lastPlayed = Time.timeSinceLevelLoad;

            Util.PlayClipAtPoint(sounds[Random.Range(0, sounds.Count)], transform.position, 1f);
        }
    }
}
