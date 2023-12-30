using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFoodScript : MonoBehaviour
{
    public GameObject cookedVersion;
    public AudioClip cookedSound;
    public void OnCook()
    {
        print("cooked");
        Util.PlayClipAtPoint(cookedSound, transform.position, 1f);
        Instantiate(cookedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
