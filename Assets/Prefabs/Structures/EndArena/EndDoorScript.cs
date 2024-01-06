using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorScript : MonoBehaviour
{
    PlayerObjectives objectives;
    private void Start()
    {
        objectives = FindAnyObjectByType<PlayerObjectives>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(objectives.GetObjectives().Count <= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
