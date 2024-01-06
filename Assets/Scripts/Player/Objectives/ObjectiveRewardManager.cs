using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveRewardManager : MonoBehaviour
{
    public AudioClip OnRewardSound;
    public float OnRewardVolume;

    public GameObject RewardViewPrefab;
    public PrefabCollection prefabmapping;

    public void OnObjectiveComplete(Objective objective)
    {
        print("objecive reward");
        Util.PlayClipAtPoint(OnRewardSound, transform.position, OnRewardVolume);
        var rewardUI = Instantiate(RewardViewPrefab, gameObject.transform);
        rewardUI.transform.localPosition = Vector3.zero;
        rewardUI.transform.localScale = Vector3.one;
        rewardUI.GetComponentInChildren<TextMeshProUGUI>().text = objective.description;

        foreach (var p in prefabmapping.prefabMappings)
        {
            if(p.PrefabMappingName == objective.reward_name)
            {
                // Instantiate(p.keyPrefab, transform.position, transform.rotation);
                Item item = new Item();
                item.PickupPrefab = p.keyPrefab;
                item.EquipPrefab = p.valuePrefab;
                item.itemName = p.PrefabMappingName;
                item.description = p.ItemDescription;
                item.image = p.image;
                item.maxInStack = p.maxInStack;
                FindAnyObjectByType<Inventory>().AddItem(item);
                rewardUI.GetComponentInChildren<Image>().sprite = p.image;
            }
        }
    }
}
