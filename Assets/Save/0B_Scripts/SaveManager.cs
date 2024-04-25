using System.IO;
using UnityEngine;
using LitJson;
using System.Text;

[System.Serializable]
public class SaveData {
    public string name;
    public object data;

    public SaveData(string name, object data) {
        this.name = name;
        this.data = data;
    }
}

public class SaveManager : MonoSingleton<SaveManager>
{
    public void Save(SaveData saveData) {
        JsonData jsonData = JsonMapper.ToJson(saveData);
        string path = Application.persistentDataPath + $"\\{saveData.name}.json";
        Debug.Log(path);
        File.WriteAllText(path, jsonData.ToString(), Encoding.UTF8);
    }

    public JsonData Load(string name) {
        string jsonString = File.ReadAllText(Application.persistentDataPath + $"\\{name}.json");
        JsonData jsonData = JsonMapper.ToObject(jsonString);
        return jsonData;
    }
}
