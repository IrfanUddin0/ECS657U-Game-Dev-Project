using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectives : MonoBehaviour
{
    [SerializeField]
    public List<Objective> objectives = new List<Objective>();
    [SerializeField]
    public Dictionary<string, string> data_map = new Dictionary<string, string>();

    private void Start()
    {
        AddObjective(new BreakTreeObjective());
        AddObjective(new DefeatEnemyObjective());
        AddObjective(new CraftCampfireObjective());
        AddObjective(new CookMeatObjective());
        AddObjective(new ExploreMineObjective());
        AddObjective(new VillagerTradeObjective());
        AddObjective(new EnemyBaseObjective());
        AddObjective(new DragonObjective());
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].CheckObjectiveStatus(data_map))
            {
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

        if(objectives.Count == 0)
            Destroy(FindAnyObjectByType<DragonScript>().gameObject);
    }

    public List<Objective> GetObjectives()
    {
        return objectives;
    }
}
