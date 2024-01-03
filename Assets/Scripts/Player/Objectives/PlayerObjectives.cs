using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerObjectives : MonoBehaviour
{
    [SerializeField]
    List<Objective> objectives = new List<Objective>();
    [SerializeField]
    Dictionary<string, string> data_map = new Dictionary<string, string>();

    private void Start()
    {
        AddObjective(new BreakTreeObjective());
        AddObjective(new DefeatEnemyObjective());
        AddObjective(new CraftCampfireObjective());
        AddObjective(new CookMeatObjective());
        AddObjective(new ExploreMineObjective());
        AddObjective(new VillagerTradeObjective());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].CheckObjectiveStatus(data_map))
            {
                // TODO: use mapping to get prefab
                // Instantiate(objectives[i].reward, transform.position, transform.rotation);
                FindAnyObjectByType<ObjectiveRewardManager>().OnObjectiveComplete(objectives[i]);
                objectives.RemoveAt(i);
                OnObjectivesUpdated();
                i--;
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
        if(!data_map.ContainsKey(key))
            data_map.Add(key, value);
    }

    public Dictionary<string, string> getDataMap()
    {
        return data_map;
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
