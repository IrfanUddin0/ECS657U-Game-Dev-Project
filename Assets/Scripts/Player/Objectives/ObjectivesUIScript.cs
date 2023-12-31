using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesUIScript : MonoBehaviour
{
    public GameObject objectivePanelPrefab;

    public void OnObjectivesUpdated()
    {
        var panel = GetComponentInChildren<VerticalLayoutGroup>();

        var objpanel1 = Instantiate(objectivePanelPrefab, panel.transform);
        objpanel1.GetComponentInChildren<TextMeshProUGUI>().text = "test";

        foreach (var objective in FindAnyObjectByType<PlayerObjectives>().GetObjectives())
        {
            GameObject objpanel = Instantiate(objectivePanelPrefab, panel.transform);
            objpanel.GetComponentInChildren<TextMeshProUGUI>().text = objective.description;
        }
    }
}
