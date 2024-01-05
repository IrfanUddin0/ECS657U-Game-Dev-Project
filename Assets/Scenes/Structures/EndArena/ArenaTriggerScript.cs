using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTriggerScript : MonoBehaviour
{
    bool playerIn = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
            playerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerIn = false;
    }

    public bool PlayerInArena()
    {
        return playerIn;
    }
}
