using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _savePanel;
    [SerializeField] private SaveInfo[] _saveInfos = new SaveInfo[3];
    [SerializeField] private Sprite _defaultSkillIcon;

    private GameObject _questionPanel;

    private void Awake() {;
        _questionPanel = _savePanel.transform.Find("QuestionBackground").gameObject;

        for(int i = 1; i <= 3; ++i) {
            string jsonData = SaveManager.Instance.Load($"Save{i}")["info"].ToJson();
            SaveInfo savedData = JsonMapper.ToObject<SaveInfo>(jsonData);

            _saveInfos[i - 1] = savedData;
            SaveAndOverwrite(i);
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
        if(Input.GetKeyDown(KeyCode.K)) {
            for(int i = 1; i <= 3; ++i) {
                Load(i);
            }
        }
    }

    private void SaveAndOverwrite(int index) {
        RenderSaveUI(index, "덮어쓰기");
    }

    private void Load(int index) {
        RenderSaveUI(index, "불러오기");
    }

    public void TitleSetting() {
        Load(1); Load(2); Load(3);
    }

    private void RenderSaveUI(int index, string buttonName) {
        SaveInfo saveInfo = _saveInfos[index - 1];

        if(saveInfo.level == -1234 && saveInfo.parts == -1234) {
            Delete(index, buttonName == "불러오기");
            return;
        }

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

        Find($"{bottomBtnPath}/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = buttonName;
        Button saveNOverwriteButton = Find($"{bottomBtnPath}/SaveNOverwriteBtn").GetComponent<Button>();
        saveNOverwriteButton.onClick.RemoveAllListeners();
        if(buttonName == "덮어쓰기")
            saveNOverwriteButton.onClick.AddListener(() => OverwriteQuestion(index));
        else
            saveNOverwriteButton.onClick.AddListener(() => LoadQuestion(index));
        Find($"{bottomBtnPath}/DeleteBtn").GetComponent<Button>().interactable = true;
        Find($"{bottomBtnPath}/DeleteBtn/Text").GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    private void Delete(int index, bool flag = false) {
        string contentPath = $"Content/Save{index}/SaveInfo/Content";
        string bottomBtnPath = $"Content/Save{index}/BottomBtn";

        Find($"Content/Save{index}/SaveInfo/Top/Date/Text").GetComponent<TextMeshProUGUI>().text = "빈 저장 슬롯";

        Find($"{contentPath}/Layout").gameObject.SetActive(false);
        Find($"{contentPath}/NullText").gameObject.SetActive(true);

        if(flag) Find($"{bottomBtnPath}/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = "불러오기";
        else Find($"{bottomBtnPath}/SaveNOverwriteBtn/Text").GetComponent<TextMeshProUGUI>().text = "저장";
        Button saveNOverwriteButton = Find($"{bottomBtnPath}/SaveNOverwriteBtn").GetComponent<Button>();
        saveNOverwriteButton.onClick.RemoveAllListeners();
        saveNOverwriteButton.onClick.AddListener(() => DeleteQuestion(index));
        if(flag) saveNOverwriteButton.interactable = false;
        else saveNOverwriteButton.interactable = true;
        Find($"{bottomBtnPath}/DeleteBtn").GetComponent<Button>().interactable = false;
        Find($"{bottomBtnPath}/DeleteBtn/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.41f, 0.41f, 0.41f);
    }

    public void SaveQuestion(int index) {
        _questionPanel.SetActive(true);
        string questionPanelPath = "QuestionBackground/QuestionPanel";
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 진행 상황을\n저장하시겠습니까?";
        Find($"{questionPanelPath}/WarningText").gameObject.SetActive(false);
        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveInfo newInfo = new SaveInfo(DateTime.Now, GearManager.Instance.GetSlotGearSO(), 
                PlayerManager.instance.playerExperience.level, PlayerManager.instance.playerExperience.currentExp, PlayerManager.instance.stats.statPoint,
                new PlayerSaveStat(PlayerManager.instance.stats), PlayerManager.instance.playerPart.Part);
            SaveData newData = new SaveData($"Save{index}", newInfo);
            SaveManager.Instance.Save(newData);
            _saveInfos[index - 1] = newInfo;
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
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 진행 상황을\n덮어쓰시겠습니까?";
        TextMeshProUGUI warningText = Find($"{questionPanelPath}/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(true);
        warningText.text = "(기존의 데이터는 삭제됩니다.)";

        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveInfo newInfo = new SaveInfo(DateTime.Now, GearManager.Instance.GetSlotGearSO(), 
                PlayerManager.instance.playerExperience.level, PlayerManager.instance.playerExperience.currentExp, PlayerManager.instance.stats.statPoint,
                new PlayerSaveStat(PlayerManager.instance.stats), PlayerManager.instance.playerPart.Part);
            SaveData newData = new SaveData($"Save{index}", newInfo);
            SaveManager.Instance.Save(newData);
            _saveInfos[index - 1] = newInfo;
            SaveAndOverwrite(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"{questionPanelPath}/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    public void LoadQuestion(int index) {
        _questionPanel.SetActive(true);
        string questionPanelPath = "QuestionBackground/QuestionPanel";
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 저장된 데이터를\n불러오시겠습니까?";
        TextMeshProUGUI warningText = Find($"{questionPanelPath}/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(false);

        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            LoadData(index);
            Load(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"{questionPanelPath}/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    public void DeleteQuestion(int index) {
        _questionPanel.SetActive(true);
        string questionPanelPath = "QuestionBackground/QuestionPanel";
        Find($"{questionPanelPath}/QuestionText").GetComponent<TextMeshProUGUI>().text = $"정말 {index}번 슬롯에 저장된 데이터를\n삭제하시겠습니까?";
        TextMeshProUGUI warningText = Find($"{questionPanelPath}/WarningText").GetComponent<TextMeshProUGUI>();
        warningText.gameObject.SetActive(true);
        warningText.text = "(삭제한 데이터는 복구할 수 없습니다.)";

        Button yesBtn = Find($"{questionPanelPath}/Btns/YesBtn").GetComponent<Button>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => {
            SaveInfo newInfo = new SaveInfo();
            SaveData newData = new SaveData($"Save{index}", newInfo);
            SaveManager.Instance.Save(newData);
            _saveInfos[index - 1] = newInfo;
            Delete(index);
            _questionPanel.SetActive(false);
        });
        Button noBtn = Find($"{questionPanelPath}/Btns/NoBtn").GetComponent<Button>();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => _questionPanel.SetActive(false));
    }

    private void LoadData(int index) {
        string jsonData = SaveManager.Instance.Load($"Save{index}")["info"].ToJson();
        SaveInfo savedData = JsonMapper.ToObject<SaveInfo>(jsonData);

        SaveManager.Instance.gearSaveInfo = savedData.gearInfos;
        SaveManager.Instance.level = savedData.level;
        SaveManager.Instance.currentExp = savedData.exp;
        SaveManager.Instance.statPoint = savedData.statPoint;
        SaveManager.Instance.atk = savedData.stat.atk;
        SaveManager.Instance.health = savedData.stat.health;
        SaveManager.Instance.defence = savedData.stat.defence;
        SaveManager.Instance.speed = savedData.stat.speed;
        SaveManager.Instance.criticalChance = savedData.stat.criticalChance;
        SaveManager.Instance.parts = savedData.parts;

        SaveManager.Instance.ReadLoad();

        // int oldGearLength = GearManager.Instance.GetSlotGearSO().Length;
        // for(int i = 0; i < oldGearLength; ++i) {
        //     GearManager.Instance.GearRemove(i);
        // }

        // for(int i = 0; i < savedData.gearInfos.Length; ++i) {
        //     GearManager.Instance.GearAdd(_gearDatabase.GetGearById(savedData.gearInfos[i].id), savedData.gearInfos[i].gearStat.GetGearStat());
        // }

        // PlayerManager.instance.playerExperience.level = savedData.level;
        // PlayerManager.instance.playerExperience.currentExp = savedData.exp;

        // PlayerStat playerStat = PlayerManager.instance.stats;
        // playerStat.statPoint = savedData.statPoint;
        // playerStat.Atk = savedData.stat.atk;
        // playerStat.Health = savedData.stat.health;
        // playerStat.Defence = savedData.stat.defence;
        // playerStat.Speed = savedData.stat.speed;
        // playerStat.CriticalChance = savedData.stat.criticalChance;

        // PlayerManager.instance.playerPart.InitPart(savedData.parts);
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
