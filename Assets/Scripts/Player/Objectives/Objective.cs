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
        reward_name = "Bow";
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
        reward_name = "Coal";
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
        reward_name = "RawMeat";
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
        reward_name = "Torch";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey("cooked");
    }
}

public class ExploreMineObjective : Objective
{
    public ExploreMineObjective()
    {
        description = "Explore a mine";
        reward_name = "Bed";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey("MineExplored");
    }
}

public class VillagerTradeObjective : Objective
{
    public VillagerTradeObjective()
    {
        description = "Make a successful trade with a villager";
        reward_name = "Pistol";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey("VillagerTrade");
    }
}