using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemChance : MonoBehaviour
{
    public GameObject dropItem;
    public float chance;
    public float spawn_up_distance;
    public AudioClip playOnDrop;

    private bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void setQuitTrue()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if(!isQuitting)
        {
            if (Util.RngDifficultyScaled(chance))
            {
                Instantiate(dropItem, transform.position+ spawn_up_distance * Vector3.up, transform.rotation);
                Util.PlayClipAtPoint(playOnDrop, transform.position, 1f);
            }
        }
    }
}
