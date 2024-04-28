using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;

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

public class SaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _savePanel;
    [SerializeField] private SaveInfo[] _saveInfos = new SaveInfo[3];
    [SerializeField] private Sprite _defaultSkillIcon;

    private GameObject _questionPanel;

    private void Awake() {
        _questionPanel = _savePanel.transform.Find("QuestionBackground").gameObject;

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

        string contentPath = $"Content/Save{index}/SaveInfo/Content";
        string bottomBtnPath = $"Content/Save{index}/BottomBtn";

        DateTime date = saveInfo.date;
        string dateString = $"{date.Year}-{date.Month:D2}-{date.Day:D2} {date.Hour:D2}:{date.Minute:D2}:{date.Second:D2}";
        Find($"Content/Save{index}/SaveInfo/Top/Date/Text").GetComponent<TextMeshProUGUI>().text = dateString;

        string spriteParentPath = $"{contentPath}/Layout/Skills";
        if(saveInfo.skillSprites != null) {
            for(int i = 1; i <= saveInfo.skillSprites.Length; ++i) {
                Find($"{spriteParentPath}/SkillIcon{i}").GetComponent<Image>().sprite = saveInfo.skillSprites[i - 1];
            }
            for(int i = saveInfo.skillSprites.Length + 1; i <= 4; ++i) {
                Find($"{spriteParentPath}/SkillIcon{i}").GetComponent<Image>().sprite = _defaultSkillIcon;
            }
        }
        else {
            for(int i = 1; i <= 4; ++i) {
                Find($"{spriteParentPath}/SkillIcon{i}").GetComponent<Image>().sprite = _defaultSkillIcon;
            }
        }

        Find($"{contentPath}/NullText").gameObject.SetActive(false);
        Find($"{contentPath}/Layout").gameObject.SetActive(true);
        Find($"{contentPath}/Layout/LevelNPart/Text").GetComponent<TextMeshProUGUI>().text = $"레벨: {saveInfo.level}\n부품: {saveInfo.parts}";

        Find($"{bottomBtnPath}/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = "덮어쓰기";
        Button saveNOverwriteButton = Find($"{bottomBtnPath}/SaveNOverwriteBtn").GetComponent<Button>();
        saveNOverwriteButton.onClick.RemoveAllListeners();
        saveNOverwriteButton.onClick.AddListener(() => OverwriteQuestion(index));
        Find($"{bottomBtnPath}/DeleteBtn").GetComponent<Button>().interactable = true;
        Find($"{bottomBtnPath}/DeleteBtn/Text").GetComponent<TextMeshProUGUI>().color = Color.black;

        SaveInfo newInfo = new SaveInfo(DateTime.Now, GearManager.Instance.GetSlotGearSO(), saveInfo.level, saveInfo.parts);
        SaveData newData = new SaveData($"Save{index}", newInfo);
        SaveManager.Instance.Save(newData);
        _saveInfos[index - 1] = newInfo;
    }

    private void Delete(int index) {
        string contentPath = $"Content/Save{index}/SaveInfo/Content";
        string bottomBtnPath = $"Content/Save{index}/BottomBtn";

        Find($"Content/Save{index}/SaveInfo/Top/Date/Text").GetComponent<TextMeshProUGUI>().text = "빈 저장 슬롯";

        Find($"{contentPath}/Layout").gameObject.SetActive(false);
        Find($"{contentPath}/NullText").gameObject.SetActive(true);

        Find($"{bottomBtnPath}/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = "저장";
        Button saveNOverwriteButton = Find($"{bottomBtnPath}/SaveNOverwriteBtn").GetComponent<Button>();
        saveNOverwriteButton.onClick.RemoveAllListeners();
        saveNOverwriteButton.onClick.AddListener(() => SaveQuestion(index));
        Find($"{bottomBtnPath}/DeleteBtn").GetComponent<Button>().interactable = false;
        Find($"{bottomBtnPath}/DeleteBtn/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.41f, 0.41f, 0.41f);
        
        SaveInfo newInfo = new SaveInfo();
        SaveData newData = new SaveData($"Save{index}", newInfo);
        SaveManager.Instance.Save(newData);
        _saveInfos[index - 1] = newInfo;
    }

    public void SaveQuestion(int index) {
        _questionPanel.SetActive(true);
        string questionPanelPath = "QuestionBackground/QuestionPanel";
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 진행 상황을\n저장하시겠습니까?";
        Find($"{questionPanelPath}/WarningText").gameObject.SetActive(false);
        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveAndOverwrite(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"{questionPanelPath}/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    public void OverwriteQuestion(int index) {
        _questionPanel.SetActive(true);
        string questionPanelPath = "QuestionBackground/QuestionPanel";
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 진행 상황을\n 덮어쓰시겠습니까?";
        TextMeshProUGUI warningText = Find($"{questionPanelPath}/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(true);
        warningText.text = "(기존의 데이터는 삭제됩니다.)";

        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveAndOverwrite(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"{questionPanelPath}/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    public void DeleteQuestion(int index) {
        _questionPanel.SetActive(true);
        string questionPanelPath = "QuestionBackground/QuestionPanel";
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 저장된 데이터를\n 삭제하시겠습니까?";
        TextMeshProUGUI warningText = Find($"{questionPanelPath}/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(true);
        warningText.text = "(삭제한 데이터는 복구할 수 없습니다.)";

        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            Delete(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"{questionPanelPath}/Btns/NoBtn").GetComponent<Button>();
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
