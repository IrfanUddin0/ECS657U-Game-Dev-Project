using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public enum InputMode
{
    Playing,
    UI
}

[Serializable]
public struct SpawnTransform
{
    public SpawnTransform(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }
    [SerializeField]
    public Vector3 pos;
    [SerializeField]
    public Quaternion rot;
}

public class MainPlayerScript : MonoBehaviour
{
    GameObject FocusedInteractableObject = null;
    public InputMode inputMode = InputMode.Playing;

    public SpawnTransform spawnTransform;

    [SerializeField]
    public InputActionReference Interact;

    public Material highQualityWaterMaterial;
    public Material lowQualityWaterMaterial;
    private int difficulty;
    void Start()
    {
        spawnTransform = new SpawnTransform(transform.position, transform.rotation);
        difficulty = PlayerPrefs.HasKey("Difficulty") ? PlayerPrefs.GetInt("Difficulty") : 0;
        SetGraphicalQuality();
        GetComponent<AudioSource>().volume = PlayerPrefs.HasKey("volume") ? PlayerPrefs.GetFloat("volume") : 1f;
    }

    private void OnEnable()
    {
        Interact.action.performed += onInteractClicked;
    }

    private void OnDisable()
    {
        Interact.action.performed -= onInteractClicked;
    }

    void Update()
    {
        checkForInteractable();
    }

    public void inputModeSetUI()
    {
        inputMode = InputMode.UI;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void inputModeSetPlaying()
    {
        inputMode = InputMode.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // method will do a linecast and look for any objects that have PlayerInteractable component
    // if exists then it will overwrite the variable FocusedInteractableObject to a ref of it
    private void checkForInteractable()
    {
        float range = 5.0f;
        float start = 0.1f;
        GameObject cam = GameObject.FindGameObjectsWithTag("CameraArm")[0];

        RaycastHit hit;
        Physics.Linecast(cam.transform.position + start * cam.transform.forward, range * cam.transform.forward + cam.transform.position, out hit);

        if (hit.transform == null || hit.transform.gameObject.GetComponent<PlayerInteractable>()==null)
            FocusedInteractableObject = null;
        else
            FocusedInteractableObject = hit.transform.gameObject;
    }

    private void onInteractClicked(InputAction.CallbackContext obj)
    {
        if (inputMode == InputMode.Playing)
        {
            if (FocusedInteractableObject != null)
                FocusedInteractableObject.GetComponent<PlayerInteractable>().OnInteract();
        }
    }

    public GameObject getFocusedInteractableObject()
    {
        return FocusedInteractableObject;
    }

    public void SetNewSpawnTransform(SpawnTransform transform)
    {
        spawnTransform = transform;
    }

    public void Respawn()
    {
        inputModeSetPlaying();
        transform.position = spawnTransform.pos;
        transform.rotation = spawnTransform.rot;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    private void SetGraphicalQuality()
    {
        // set quality level
        int qualitylevel = PlayerPrefs.HasKey("HighSettings") ? PlayerPrefs.GetInt("HighSettings") : 0;
        QualitySettings.SetQualityLevel(qualitylevel, true);
        print(QualitySettings.names[0]);
        // disable pp
        GetComponentInChildren<PostProcessLayer>().enabled = (qualitylevel==0);
        GetComponentInChildren<PostProcessVolume>().enabled = (qualitylevel==0);
        // disable water shader
        FindAnyObjectByType<WaterInteractScript>().gameObject.GetComponent<MeshRenderer>().material = (qualitylevel == 0) ? highQualityWaterMaterial : lowQualityWaterMaterial;
    }
}
