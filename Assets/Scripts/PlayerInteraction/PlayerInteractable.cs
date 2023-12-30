using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    public string interactPromt;
    public AudioClip onInteractSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnInteract()
    {
        Util.PlayClipAtPoint(onInteractSound, GameObject.FindGameObjectWithTag("Player").transform.position, 1f);
        print("Playing interacting");
    }
}
