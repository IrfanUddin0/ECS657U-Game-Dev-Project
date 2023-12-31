using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour
{
    List<Objective> objectives = new List<Objective>();
    Dictionary<string, string> data_map = new Dictionary<string, string>();

    private void Start()
    {
        OnObjectivesUpdated();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].CheckObjectiveStatus())
            {
                Instantiate(objectives[i].reward, transform.position, transform.rotation);
                Destroy(objectives[i]);
                OnObjectivesUpdated();
            }
        }
    }
    public void AddObjective(Objective objective)
    {
        objectives.Add(objective);
        OnObjectivesUpdated();
    }

    public void addDataEntry(string key, string value)
    {
        data_map.Add(key, value);
    }

    public void OnObjectivesUpdated()
    {
        FindAnyObjectByType<ObjectivesUIScript>().OnObjectivesUpdated();
    }

    public List<Objective> GetObjectives()
    {
        return objectives;
    }
}
