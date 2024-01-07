using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public string description;
    public string reward_name;
    public string data_key_name;
    public bool CheckObjectiveStatus(Dictionary<string, string> dataMap)
    {
        return dataMap.ContainsKey(data_key_name);
        /*
        if(dataMap.ContainsKey(data_key_name))
            return true;

        
        foreach(var entry in dataMap)
        {
            if (entry.Key.Contains(data_key_name))
            {
                return true;
            }
        }

        return false;
        */
    }
}


[System.Serializable]
public class BreakTreeObjective : Objective
{
    public BreakTreeObjective()
    {
        description = "Break a tree";
        reward_name = "Bow";
        data_key_name = "TreePrefabDestroyed";
    }
}

[System.Serializable]
public class DefeatEnemyObjective : Objective
{
    public DefeatEnemyObjective()
    {
        description = "Defeat an enemy";
        reward_name = "Coal";
        data_key_name = "EnemyDefeated";
    }
}

[System.Serializable]
public class CraftCampfireObjective : Objective
{
    public CraftCampfireObjective()
    {
        description = "Craft a campfire (Wood, Coal)";
        reward_name = "RawMeat";
        data_key_name = "CampfireCrafted";
    }
}

[System.Serializable]
public class CookMeatObjective : Objective
{
    public CookMeatObjective()
    {
        description = "Cook meat using the campfire";
        reward_name = "Torch";
        data_key_name = "cooked";
    }
}

[System.Serializable]
public class ExploreMineObjective : Objective
{
    public ExploreMineObjective()
    {
        description = "Explore a mine";
        reward_name = "Bed";
        data_key_name = "MineExplored";
    }
}

[System.Serializable]
public class VillagerTradeObjective : Objective
{
    public VillagerTradeObjective()
    {
        description = "Make a successful trade with a villager";
        reward_name = "Pistol";
        data_key_name = "VillagerTrade";
    }
}

[System.Serializable]
public class EnemyBaseObjective : Objective
{
    public EnemyBaseObjective()
    {
        description = "Infiltrate an enemy base";
        reward_name = "Smg";
        data_key_name = "EnteredBase";
    }
}

[System.Serializable]
public class DragonObjective : Objective
{
    public DragonObjective()
    {
        description = "Kill the final dragon boss";
        reward_name = "Dragon Sword";
        data_key_name = "DragonKilled";
    }
}