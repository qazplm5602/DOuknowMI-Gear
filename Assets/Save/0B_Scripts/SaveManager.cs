using System;
using System.IO;
using System.Text;
using UnityEngine;
using LitJson;

[Serializable]
public class SaveData {
    public string name;
    public SaveInfo info;

    public SaveData(string name, SaveInfo info) {
        this.name = name;
        this.info = info;
    }
}

[Serializable]
public class SaveInfo {
    public DateTime date;
    public Sprite[] skillSprites;
    public GearSaveInfo[] gearInfos;
    public int level;
    public int exp;
    public int statPoint;
    public PlayerSaveStat stat;
    public int parts;

    public SaveInfo() {
        level = -1234;
        parts = -1234;
    }

    public SaveInfo(DateTime date, GearGroupDTO[] gearGroup, int level, int exp, int statPoint, PlayerSaveStat stat, int parts) {
        this.date = date;
        skillSprites = new Sprite[gearGroup.Length];
        gearInfos = new GearSaveInfo[gearGroup.Length];

        for(int i = 0; i < gearGroup.Length; ++i) {
            skillSprites[i] = gearGroup[i].data.Icon;
            gearInfos[i] = new GearSaveInfo() {
                id = gearGroup[i].data.id,
                gearStat = new GearSaveStat(gearGroup[i].stat)
            };
        }

        this.level = level;
        this.exp = exp;
        this.statPoint = statPoint;
        this.stat = stat;
        this.parts = parts;
    }
}

[Serializable]
public class GearSaveInfo {
    public string id;
    public GearSaveStat gearStat;
}

[Serializable]
public class GearSaveStat {
    public int level;
    public int damage;
    public double range;

    public GearSaveStat(GearStat stat) {
        level = stat.level;
        damage = stat.damage;
        range = stat.range;
    }

    public GearStat GetGearStat() {
        GearStat gearStat = new GearStat() {
            level = level,
            damage = damage,
            range = (float)range
        };
        return gearStat;
    }
}

[Serializable]
public class PlayerSaveStat {
    public int atk;
    public int health;
    public int defence;
    public int speed;
    public int criticalChance;

    public PlayerSaveStat(int atk, int health, int defence, int speed, int critChance) {
        this.atk = atk;
        this.health = health;
        this.defence = defence;
        this.speed = speed;
        criticalChance = critChance;
    }

    public PlayerSaveStat(PlayerStat playerStat) {
        atk = playerStat.Atk;
        health = playerStat.Health;
        defence = playerStat.Defence;
        speed = playerStat.Speed;
        criticalChance = playerStat.CriticalChance;
    }
}

public class SaveManager : MonoSingleton<SaveManager>
{
    public SaveUI saveUI;

    public GearDatabase gearDataBase;

    public GearSaveInfo[] gearSaveInfo;
    public int level;
    public int currentExp;
    public int statPoint;
    public int atk;
    public int health;
    public int defence;
    public int speed;
    public int criticalChance;
    public int parts;

    private void Awake() {
        saveUI = GetComponent<SaveUI>();
    }

    public void Save(SaveData saveData) {
        JsonData jsonData = JsonMapper.ToJson(saveData);
        string path = Application.persistentDataPath + $"\\{saveData.name}.json";
        File.WriteAllText(path, jsonData.ToString(), Encoding.UTF8);
    }

    public JsonData Load(string name) {
        string jsonString;
        string path = Application.persistentDataPath + $"\\{name}.json";
        try {
            jsonString = File.ReadAllText(path);
        }
        catch {
            JsonData newData = JsonMapper.ToJson(new SaveData(name, new SaveInfo()));
            File.WriteAllText(path, newData.ToString(), Encoding.UTF8);
            jsonString = File.ReadAllText(path);
        }
        JsonData jsonData = JsonMapper.ToObject(jsonString);
        return jsonData;
    }

    public void ReadLoad() {
        TitleManager.Instance.ChangeSceneToVillage();

        int oldGearLength = GearManager.Instance.GetSlotGearSO().Length;
        for(int i = 0; i < oldGearLength; ++i) {
            GearManager.Instance.GearRemove(i);
        }
        for(int i = 0; i < gearSaveInfo.Length; ++i) {
            GearManager.Instance.GearAdd(gearDataBase.GetGearById(gearSaveInfo[i].id), gearSaveInfo[i].gearStat.GetGearStat());
        }

        PlayerManager.instance.playerExperience.level = level;
        PlayerManager.instance.playerExperience.currentExp = currentExp;

        PlayerStat playerStat = PlayerManager.instance.stats;
        playerStat.statPoint = statPoint;
        playerStat.Atk = atk;
        playerStat.Health = health;
        playerStat.Defence = defence;
        playerStat.Speed = speed;
        playerStat.CriticalChance = criticalChance;

        PlayerManager.instance.playerPart.InitPart(parts);
    }
}
