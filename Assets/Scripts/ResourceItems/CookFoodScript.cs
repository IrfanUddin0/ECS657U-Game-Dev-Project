using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFoodScript : MonoBehaviour
{
    public GameObject cookedVersion;

    public void OnCook()
    {
        print("cooked");
        Instantiate(cookedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
