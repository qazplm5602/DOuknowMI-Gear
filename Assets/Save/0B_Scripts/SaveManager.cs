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
    public int parts;

    public SaveInfo() {
        level = -1234;
        parts = -1234;
    }

    public SaveInfo(DateTime date, GearGroupDTO[] gearGroup, int level, int parts) {
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

public class SaveManager : MonoSingleton<SaveManager>
{
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
}
