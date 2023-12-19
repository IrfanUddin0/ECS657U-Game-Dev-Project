using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SetTextToInputActionKey : MonoBehaviour
{
    public string actionName;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput input = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInput>();
        string a = input.actions[actionName].bindings[0].path;
        a = a.Split('/').Last();
        GetComponent<TextMeshProUGUI>().text = a;
    }
}
