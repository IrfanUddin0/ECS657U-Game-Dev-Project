using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRotateScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3 (0f, 0f, GameObject.FindGameObjectsWithTag("Player")[0].transform.eulerAngles.y);
    }
}
