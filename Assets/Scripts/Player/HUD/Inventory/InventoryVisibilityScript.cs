using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryVisibilityScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InventoryKey"))
            if ((GetComponent<RectTransform>().localScale == Vector3.zero))
                show();
            else
                hide();
    }

    void show()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputModeSetUI();
    }

    void hide()
    {
        GetComponent<RectTransform>().localScale = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputModeSetPlaying();
    }
}
