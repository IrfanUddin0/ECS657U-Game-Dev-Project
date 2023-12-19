using UnityEngine;
using UnityEngine.UI;

public class EntityHPBarScript : MonoBehaviour
{
    public float range;
    // Update is called once per frame
    void Update()
    {
        GameObject cam = GameObject.FindGameObjectsWithTag("CameraArm")[0];

        RaycastHit hit;
        Physics.Linecast(cam.transform.position + 1.0f * cam.transform.forward, range * cam.transform.forward + cam.transform.position, out hit);

        PlayerHittable hittable = null;
        if(hit.transform != null)
        {
            hittable = hit.transform.GetComponentInChildren<PlayerHittable>();
            if(hittable == null )
                hittable = hit.transform.GetComponentInParent<PlayerHittable>();
        }
        
        if(hittable != null )
        {
            GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(160.0f, 20.0f);
            GetComponentInChildren<Slider>().value = hittable.health / hittable.maxHealth;
            Vector3 pos = cam.GetComponentInChildren<Camera>().WorldToScreenPoint(hittable.GetComponentInChildren<Collider>().bounds.center);
            GetComponentInChildren<RectTransform>().position = pos;
        }
        else
        {
            GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
        }

    }
}
