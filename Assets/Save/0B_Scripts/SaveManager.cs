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
