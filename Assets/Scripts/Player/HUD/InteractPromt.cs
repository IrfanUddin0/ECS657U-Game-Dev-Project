using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPromt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MainPlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MainPlayerScript>();
        if(player.getFocusedInteractableObject()!=null )
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            string interactName = player.getFocusedInteractableObject().name;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Interact: "+interactName;
        }
        else
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }
    }
}
