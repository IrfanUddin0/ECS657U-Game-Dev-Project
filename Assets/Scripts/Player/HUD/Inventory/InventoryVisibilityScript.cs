using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryVisibilityScript : MonoBehaviour
{
    bool shown = false;

    [SerializeField]
    public InputActionReference InventoryKey;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryKey.action.performed += action => InventoryKeyPressed();
    }

    private void OnDisable()
    {
        InventoryKey.action.performed -= action => InventoryKeyPressed();
    }

    private void InventoryKeyPressed()
    {
        if (!shown)
            show();
        else
            hide();
    }

    public void show()
    {
        shown = true;
        transform.GetChild(0).gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputModeSetUI();
    }

    public void hide()
    {
        shown = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<MainPlayerScript>().inputModeSetPlaying();
    }
}
