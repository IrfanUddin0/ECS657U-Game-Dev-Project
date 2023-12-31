using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Objective
{
    [SerializeField]
    public string description;
    [SerializeField]
    public string reward_name;
    public abstract bool CheckObjectiveStatus(Dictionary<string, string> dataMap);
}


public class BreakTreeObjective : Objective
{
    public BreakTreeObjective()
    {
        description = "Break a tree";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        foreach(var entry in dataMap)
        {
            if(entry.Key.Contains("Tree") && entry.Value.Contains("destroyed"))
            {
                return true;
            }
        }
        return false;
    }
}

public class DefeatEnemyObjective : Objective
{
    public DefeatEnemyObjective()
    {
        description = "Defeat an enemy";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        foreach (var entry in dataMap)
        {
            if (entry.Key.Contains("Enemy") && entry.Value.Contains("defeated"))
            {
                return true;
            }
        }
        return false;
    }
}

public class CraftCampfireObjective : Objective
{
    public CraftCampfireObjective()
    {
        description = "Craft a campfire (Wood, Coal)";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        foreach (var entry in dataMap)
        {
            if (entry.Key.Contains("Campfire") && entry.Value.Contains("crafted"))
            {
                return true;
            }
        }
        return false;
    }
}

public class CookMeatObjective : Objective
{
    public CookMeatObjective()
    {
        description = "Cook meat using the campfire";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey("cooked");
    }
}