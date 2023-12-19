using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPromt : MonoBehaviour
{
    private string IteractKey;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput input = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInput>();
        IteractKey = input.actions["Interact"].bindings[0].path;
        IteractKey = IteractKey.Split('/').Last();
    }

    // Update is called once per frame
    void Update()
    {
        MainPlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MainPlayerScript>();
        if(player.getFocusedInteractableObject()!=null )
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            string interactName = player.getFocusedInteractableObject().GetComponentInChildren<PlayerInteractable>().interactPromt;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "[" + IteractKey + "] " +interactName;
        }
        else
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }
    }
}
