using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;

[Serializable]
public class SaveInfo {
    public DateTime date;
    public SkillController[] _skills;
    public int level;
    public int parts;

    public SaveInfo() {
        level = -1234;
        parts = -1234;
    }

    public SaveInfo(DateTime date, int level, int parts) {
        this.date = date;
        this.level = level;
        this.parts = parts;
    }
}

public class SaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _savePanel;
    [SerializeField] private SaveInfo[] _saveInfos = new SaveInfo[3];
    [SerializeField] private GameObject _questionPanel;

    private void Awake() {
        for(int i = 1; i <= 3; ++i) {
            string jsonData = SaveManager.Instance.Load($"Save{i}")["info"].ToJson();
            SaveInfo savedData = JsonMapper.ToObject<SaveInfo>(jsonData);

            _saveInfos[i - 1] = savedData;
            if(savedData.level == -1234 && savedData.parts == -1234) {
                Delete(i);
            }
            else {
                SaveAndOverwrite(i);
            }
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            _savePanel.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.O)) {
            SaveAndOverwrite(1);
        }
        if(Input.GetKeyDown(KeyCode.L)) {
            Delete(1);
        }
    }

    private void SaveAndOverwrite(int index) {
        SaveInfo saveInfo = _saveInfos[index - 1];

        saveInfo.date = DateTime.Now;
        saveInfo.level = 1;
        saveInfo.parts = 10;

        DateTime date = saveInfo.date;
        string dateString = $"{date.Year}-{date.Month:D2}-{date.Day:D2} {date.Hour:D2}:{date.Minute:D2}:{date.Second:D2}";
        Find($"Content/Save{index}/SaveInfo/Top/Date/Text").GetComponent<TextMeshProUGUI>().text = dateString;

        Find($"Content/Save{index}/SaveInfo/Content/NullText").gameObject.SetActive(false);
        Find($"Content/Save{index}/SaveInfo/Content/Layout").gameObject.SetActive(true);
        Find($"Content/Save{index}/SaveInfo/Content/Layout/LevelNPart/Text").GetComponent<TextMeshProUGUI>().text = $"레벨: {saveInfo.level}\n부품: {saveInfo.parts}";

        Find($"Content/Save{index}/BottomBtn/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = "덮어쓰기";
        Button saveNOverwriteButton = Find($"Content/Save{index}/BottomBtn/SaveNOverwriteBtn").GetComponent<Button>();
        saveNOverwriteButton.onClick.RemoveAllListeners();
        saveNOverwriteButton.onClick.AddListener(() => OverwriteQuestion(index));
        Find($"Content/Save{index}/BottomBtn/DeleteBtn").GetComponent<Button>().interactable = true;
        Find($"Content/Save{index}/BottomBtn/DeleteBtn/Text").GetComponent<TextMeshProUGUI>().color = Color.black;

        SaveInfo newInfo = new SaveInfo(DateTime.Now, saveInfo.level, saveInfo.parts);
        SaveData newData = new SaveData($"Save{index}", newInfo);
        SaveManager.Instance.Save(newData);
        _saveInfos[index - 1] = newInfo;
    }

    private void Delete(int index) {
        Find($"Content/Save{index}/SaveInfo/Top/Date/Text").GetComponent<TextMeshProUGUI>().text = "빈 저장 슬롯";

        Find($"Content/Save{index}/SaveInfo/Content/Layout").gameObject.SetActive(false);
        Find($"Content/Save{index}/SaveInfo/Content/NullText").gameObject.SetActive(true);

        Find($"Content/Save{index}/BottomBtn/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = "저장";
        Button saveNOverwriteButton = Find($"Content/Save{index}/BottomBtn/SaveNOverwriteBtn").GetComponent<Button>();
        saveNOverwriteButton.onClick.RemoveAllListeners();
        saveNOverwriteButton.onClick.AddListener(() => SaveQuestion(index));
        Find($"Content/Save{index}/BottomBtn/DeleteBtn").GetComponent<Button>().interactable = false;
        Find($"Content/Save{index}/BottomBtn/DeleteBtn/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.41f, 0.41f, 0.41f);
        
        SaveInfo newInfo = new SaveInfo();
        SaveData newData = new SaveData($"Save{index}", newInfo);
        SaveManager.Instance.Save(newData);
        _saveInfos[index - 1] = newInfo;
    }

    public void SaveQuestion(int index) {
        _questionPanel.SetActive(true);
        Find($"QuestionBackground/QuestionPanel/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 진행 상황을\n저장하시겠습니까?";
        Find($"QuestionBackground/QuestionPanel/WarningText").gameObject.SetActive(false);
        Button yesBtn = Find($"QuestionBackground/QuestionPanel/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveAndOverwrite(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"QuestionBackground/QuestionPanel/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    public void OverwriteQuestion(int index) {
        _questionPanel.SetActive(true);
        Find($"QuestionBackground/QuestionPanel/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 진행 상황을\n 덮어쓰시겠습니까?";
        TextMeshProUGUI warningText = Find($"QuestionBackground/QuestionPanel/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(true);
        warningText.text = "(기존의 데이터는 삭제됩니다.)";
        Button yesBtn = Find($"QuestionBackground/QuestionPanel/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveAndOverwrite(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"QuestionBackground/QuestionPanel/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    public void DeleteQuestion(int index) {
        _questionPanel.SetActive(true);
        Find($"QuestionBackground/QuestionPanel/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 저장된 데이터를\n 삭제하시겠습니까?";
        TextMeshProUGUI warningText = Find($"QuestionBackground/QuestionPanel/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(true);
        warningText.text = "(삭제한 데이터는 복구할 수 없습니다.)";
        Button yesBtn = Find($"QuestionBackground/QuestionPanel/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            Delete(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"QuestionBackground/QuestionPanel/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    private Transform Find(string path) {
        Transform parent = _savePanel.transform;
        string[] child = path.Split('/');
        for(int i = 0; i < child.Length; ++i) {
            try {
                parent = parent.Find(child[i]);
            }
            catch(Exception ex) {
                Debug.LogError($"Not Found Path : {child[i]} in {path}");
                Debug.LogError(ex.Message);
            }
        }
        return parent;
    }

    public void Exit() {
        _savePanel.SetActive(false);
    }
}
