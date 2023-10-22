using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItemEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("spawned equippable item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnFireClicked()
    {

    }

    public virtual void OnFireHeld()
    {

    }

    public virtual void OnFireReleased()
    {

    }

    public virtual void OnADSClicked()
    {

    }

    public virtual void OnADSReleased()
    {

    }

    public virtual void OnReloadClicked()
    {

    }

    public virtual void OnReloadCanceled()
    {

    }
}
