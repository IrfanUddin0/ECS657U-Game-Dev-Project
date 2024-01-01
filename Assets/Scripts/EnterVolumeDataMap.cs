using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVolumeDataMap : MonoBehaviour
{
    public string dataKey;
    public string dataValue;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindAnyObjectByType<PlayerObjectives>().addDataEntry(dataKey, dataValue);
        }
    }
}
