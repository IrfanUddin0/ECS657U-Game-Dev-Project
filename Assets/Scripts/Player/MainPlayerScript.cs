using UnityEngine;
public enum InputMode
{
    Playing,
    UI
}

public class MainPlayerScript : MonoBehaviour
{
    GameObject FocusedInteractableObject = null;
    public InputMode inputMode = InputMode.Playing;

    void Start()
    {
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
    }

    public void inputModeSetPlaying()
    {
        inputMode = InputMode.Playing;
    }

    // method will do a linecast and look for any objects that have PlayerInteractable component
    // if exists then it will overwrite the variable FocusedInteractableObject to a ref of it
    private void checkForInteractable()
    {
        float range = 10.0f;
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
}
