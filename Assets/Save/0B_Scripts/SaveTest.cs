using UnityEngine;

public class SaveTest : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)) {
            SaveManager.Instance.Save(new SaveData("asdf", "1"));
        }
        else if(Input.GetKeyDown(KeyCode.U)) {
            Debug.Log(SaveManager.Instance.Load("asdf")["data"].ToString());
        }
    }
}
