using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public string description;
    public string reward_name;
    public virtual bool CheckObjectiveStatus(Dictionary<string, string> dataMap) { return false; }
}


[System.Serializable]
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

[System.Serializable]
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

[System.Serializable]
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

[System.Serializable]
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

[System.Serializable]
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

[System.Serializable]
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

[System.Serializable]
public class EnemyBaseObjective : Objective
{
    public EnemyBaseObjective()
    {
        description = "Infiltrate an enemy base";
        reward_name = "Smg";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey("EnteredBase");
    }
}

[System.Serializable]
public class DragonObjective : Objective
{
    public DragonObjective()
    {
        description = "Kill the final dragon boss";
        reward_name = "Smg";
    }
    public override bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey("DragonKilled");
    }
}