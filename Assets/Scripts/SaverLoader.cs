using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerTransformSave
{
    [SerializeField]
    public Vector3 pos;
    [SerializeField]
    public Vector3 rot;
}

[Serializable]
public class CameraTransformSave
{
    [SerializeField]
    public Vector3 cameraRot;
}

[Serializable]
public class PlayerSurvivalSave
{
    public float health;
    public float maxHealth;

    // percentages, max is 1.0f
    public float hunger;
    public float thirst;

    public float hungerDecreaseRate;
    public float thirstDecreaseRate;
    public float healthDecreaseRate;
}

[Serializable]
public class ScoreManagerSave
{
    [SerializeField]
    public int scoreCount;
    [SerializeField]
    public bool countNext;
    [SerializeField]
    public float lastDeathXLightPosition;
    [SerializeField]
    public float lastDeathYLightPosition;
}

[Serializable]
public class SavedObjectDetails
{
    [SerializeField]
    public String mappingName;
    [SerializeField]
    public Vector3 pos;
    [SerializeField]
    public Quaternion rot;
}

[Serializable]
public class PlacedObjectsSave
{
    [SerializeField]
    public List<SavedObjectDetails> objects = new List<SavedObjectDetails>();
}

public class SaverLoader : MonoBehaviour
{
    public PlacedItemCollection placedItemCollection;
    public float updateRate = 5f;
    public bool DEBUGResetsave = false;
    private float lastUpdateTime;
    // load everything from json
    void Start()
    {
        // if a key is missing then dont load
        if (!SaveExists() || DEBUGResetsave)
        {
            DeleteSave();
            return;
        }

        // load player inventory
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("Inventory"), FindAnyObjectByType<Inventory>());
        FindAnyObjectByType<Inventory>().OnInventoryUpdated();
        FindAnyObjectByType<InventoryEquipManager>().ChangeEquipIndex(1); FindAnyObjectByType<InventoryEquipManager>().ChangeEquipIndex(0);

        // load player survival
        PlayerSurvivalSave sa = JsonUtility.FromJson<PlayerSurvivalSave>(PlayerPrefs.GetString("PlayerSurvival"));
        var ps = FindAnyObjectByType<PlayerSurvival>();
        ps.health = sa.health;
        ps.maxHealth = sa.maxHealth;
        ps.hunger = sa.hunger;
        ps.thirst = sa.thirst;
        ps.hungerDecreaseRate = sa.hungerDecreaseRate;
        ps.thirstDecreaseRate = sa.thirstDecreaseRate;
        ps.healthDecreaseRate = sa.healthDecreaseRate;

        // load spawn transform
        SpawnTransform st = JsonUtility.FromJson<SpawnTransform>(PlayerPrefs.GetString("Spawn"));
        FindAnyObjectByType<MainPlayerScript>().SetNewSpawnTransform(st);

        // load player transform
        PlayerTransformSave pts = JsonUtility.FromJson<PlayerTransformSave>(PlayerPrefs.GetString("PlayerTransform"));
        transform.position = pts.pos;
        transform.localEulerAngles = pts.rot;

        // load camera rotation
        CameraTransformSave cts = JsonUtility.FromJson<CameraTransformSave>(PlayerPrefs.GetString("CameraRot"));
        GameObject.FindGameObjectsWithTag("CameraArm")[0].transform.localEulerAngles = cts.cameraRot;

        // load sun anim pos
        FindAnyObjectByType<SkyboxFogColor>().gameObject.GetComponent<Animator>().SetFloat("AnimPos", PlayerPrefs.GetFloat("SunPos"));

        // load score
        FindAnyObjectByType<ScoreManager>().setScoreFromSave(JsonUtility.FromJson<ScoreManagerSave>(PlayerPrefs.GetString("Score")));

        // spawn all saved objects

        // first remove current placed and pickupable objects
        PlacedItem[] placedobjs = FindObjectsByType<PlacedItem>(FindObjectsSortMode.None);
        foreach (PlacedItem obj in placedobjs)
        {
            Destroy(obj.gameObject);
        }
        PickupItem[] pickupobjs = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
        foreach (PickupItem obj in pickupobjs)
        {
            Destroy(obj.gameObject);
        }

        PlacedObjectsSave objssave = JsonUtility.FromJson<PlacedObjectsSave>(PlayerPrefs.GetString("WorldObjects"));
        foreach (var obj in objssave.objects)
        {
            var map = placedItemCollection.findWithName(obj.mappingName);
            Instantiate(map.Prefab, obj.pos, obj.rot);
        }

        // load objectives
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("Objectives"), FindAnyObjectByType<PlayerObjectives>());
        FindAnyObjectByType<PlayerObjectives>().OnObjectivesUpdated();

        lastUpdateTime = Time.timeSinceLevelLoad;
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad - lastUpdateTime >= updateRate)
        {
            Save();
            lastUpdateTime = Time.timeSinceLevelLoad;
        }
    }

    public void Save()
    {
        // save player inventory
        PlayerPrefs.SetString("Inventory", JsonUtility.ToJson(FindAnyObjectByType<Inventory>()));

        // save player survival
        var ps = FindAnyObjectByType<PlayerSurvival>();
        PlayerSurvivalSave ps_save = new PlayerSurvivalSave();
        ps_save.health = ps.health;
        ps_save.maxHealth = ps.maxHealth;
        ps_save.hunger = ps.hunger;
        ps_save.thirst = ps.thirst;
        ps_save.hungerDecreaseRate = ps.hungerDecreaseRate;
        ps_save.thirstDecreaseRate = ps.thirstDecreaseRate;
        ps_save.healthDecreaseRate = ps.healthDecreaseRate;
        PlayerPrefs.SetString("PlayerSurvival", JsonUtility.ToJson(ps_save));

        // save player spawn pos
        PlayerPrefs.SetString("Spawn", JsonUtility.ToJson(FindAnyObjectByType<MainPlayerScript>().spawnTransform));

        // save player transform
        PlayerTransformSave pts = new PlayerTransformSave();
        pts.pos = transform.position;
        pts.rot = transform.localEulerAngles;
        PlayerPrefs.SetString("PlayerTransform", JsonUtility.ToJson(pts));

        // save camera rotation
        CameraTransformSave cts = new CameraTransformSave();
        cts.cameraRot = GameObject.FindGameObjectsWithTag("CameraArm")[0].transform.localEulerAngles;
        PlayerPrefs.SetString("CameraRot", JsonUtility.ToJson(cts));

        // save sun anim pos
        PlayerPrefs.SetFloat("SunPos", FindAnyObjectByType<SkyboxFogColor>().gameObject.GetComponent<Animator>().GetFloat("AnimPos"));

        // save score
        ScoreManagerSave sms = new ScoreManagerSave();
        var sm = FindAnyObjectByType<ScoreManager>();
        sms.scoreCount = sm.scoreCount;
        sms.countNext = sm.countNext;
        sms.lastDeathXLightPosition = sm.lastDeathXLightPosition;
        sms.lastDeathYLightPosition = sm.lastDeathYLightPosition;
        PlayerPrefs.SetString("Score", JsonUtility.ToJson(sms));

        // save all placed items
        PlacedItem[] objs = FindObjectsByType<PlacedItem>(FindObjectsSortMode.None);
        PlacedObjectsSave newobjsSave = new PlacedObjectsSave();

        foreach (PlacedItem obj in objs)
        {
            SavedObjectDetails newdetail = new SavedObjectDetails();
            newdetail.mappingName = obj.SaveName;
            newdetail.pos = obj.transform.position;
            newdetail.rot = obj.transform.rotation;
            newobjsSave.objects.Add(newdetail);
        }

        PlayerPrefs.SetString("WorldObjects", JsonUtility.ToJson(newobjsSave));


        // save objectives
        PlayerPrefs.SetString("Objectives", JsonUtility.ToJson(FindAnyObjectByType<PlayerObjectives>()));

        PlayerPrefs.Save();
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey("Inventory");
        PlayerPrefs.DeleteKey("PlayerSurvival");
        PlayerPrefs.DeleteKey("Spawn");
        PlayerPrefs.DeleteKey("PlayerTransform");
        PlayerPrefs.DeleteKey("CameraRot");
        PlayerPrefs.DeleteKey("SunPos");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("WorldObjects");
        PlayerPrefs.DeleteKey("Objectives");

        PlayerPrefs.Save();
    }

    public static bool SaveExists()
    {
        return
            PlayerPrefs.HasKey("Inventory")
            && PlayerPrefs.HasKey("PlayerSurvival")
            && PlayerPrefs.HasKey("Spawn")
            && PlayerPrefs.HasKey("PlayerTransform")
            && PlayerPrefs.HasKey("CameraRot")
            && PlayerPrefs.HasKey("SunPos")
            && PlayerPrefs.HasKey("Score")
            && PlayerPrefs.HasKey("WorldObjects")
            && PlayerPrefs.HasKey("Objectives");
    }
}
