using UnityEngine;

public enum InputMode
{
    Playing,
    UI
}

public struct SpawnTransform
{
    public SpawnTransform(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }
    public Vector3 pos;
    public Quaternion rot;
}

public class MainPlayerScript : MonoBehaviour
{
    GameObject FocusedInteractableObject = null;
    public InputMode inputMode = InputMode.Playing;

    public SpawnTransform spawnTransform;
    void Start()
    {
        spawnTransform = new SpawnTransform(transform.position, transform.rotation);
    }


    void Update()
    {
        if(inputMode == InputMode.Playing)
        {
            checkForInteractable();
            if (Input.GetButtonDown("Interact"))
            {
                onInteractClicked();
            }
        }
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

    private void onInteractClicked()
    {
        if (FocusedInteractableObject != null)
            FocusedInteractableObject.GetComponent<PlayerInteractable>().OnInteract();
    }

    public GameObject getFocusedInteractableObject()
    {
        return FocusedInteractableObject;
    }

    public void Respawn()
    {
        inputModeSetPlaying();
        transform.position = spawnTransform.pos;
        transform.rotation = spawnTransform.rot;
    }
}
