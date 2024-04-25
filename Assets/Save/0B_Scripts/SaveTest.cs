using System;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)) {
            SaveManager.Instance.Save(new SaveData("Save1", new SaveInfo(DateTime.Now, 1, 10)));
        }
        else if(Input.GetKeyDown(KeyCode.U)) {
            Debug.Log(SaveManager.Instance.Load("Save1")["data"].ToString());
        }
    }
}
