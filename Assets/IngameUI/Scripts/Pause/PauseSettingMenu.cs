using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct PauseSettingSOPair {
    public string category;
    public PauseSettingSO so;
}

public class PauseSettingMenu : MonoBehaviour
{
    // 이벤트 등록할때 Start에서 등록하는걸 매우 권장함 (Awake에서 초기화함)
    static Dictionary<string, Action<string>> OnSetValue;
    static Dictionary<string, Func<string>> OnGetValue;

    [Header("Section")]
    [SerializeField] GameObject _mainScreen;
    [SerializeField] Transform _categorys;
    [SerializeField] Transform _scrollContent;
    [SerializeField] TextMeshProUGUI _description;

    [Header("Prefab")]
    [SerializeField] Button _sectionBtn;
    [SerializeField] PauseSettingBox _box;
    
    [Header("Config")]
    [SerializeField] PauseSettingSOPair[] menus;

    public void Show() {
        _mainScreen.SetActive(true);
    }
    public void Hide() {
        _mainScreen.SetActive(false);
    }

    private void Awake() {
        OnSetValue = new();
        OnGetValue = new();

        for (int i = 0; i < menus.Length; i++)
        {
            int idx = i;
            var item = menus[i];

            Button menu = Instantiate(_sectionBtn, _categorys);
            menu.onClick.AddListener(() => ClickCategory(idx));
            
            var text = menu.GetComponent<TextMeshProUGUI>();
            text.color = new Color32(128, 128, 128, 255);
            text.text = item.category;
        }
    }

    private void OnDestroy() {
        OnSetValue = null;
        OnGetValue = null;
    }

    void ClickCategory(int idx) {
        int i = 0;
        foreach (Transform item in _categorys)
        {
            item.GetComponent<TextMeshProUGUI>().color = (i == idx) ? Color.white : new Color32(128, 128, 128, 255);
            i ++;
        }

        // 콘텐츠 안에 있는거 다 지움
        foreach (Transform item in _scrollContent)
            Destroy(item.gameObject);

        foreach (var item in menus[idx].so.datas)
        {
            var box = Instantiate(_box, _scrollContent);
            box.Init(this, item, () => SetDescription(item.description), () => SetDescription());
        }
    }

    void SetDescription(string description = null) {
        _description.text = description == null ? "설명을 보려면 박스를 마우스에 올려두세요." : description;
    }

    //////////////////////// EVENT

    public void AddSetEvent(string id, Action<string> callback) {
        if (OnSetValue.TryGetValue(id, out var _)) {
            OnSetValue[id] += callback;
        } else {
            OnSetValue[id] = callback;
        }
    }
    
    public void RemoveSetEvent(string id, Action<string> callback) {
        if (OnSetValue == null) return;
        OnSetValue[id] -= callback;
    }

    public void AddGetEvent(string id, Func<string> callback) {
        if (OnGetValue.TryGetValue(id, out var _)) {
            OnGetValue[id] += callback;
        } else {
            OnGetValue[id] = callback;
        }
    }
    
    public void RemoveGetEvent(string id, Func<string> callback) {
        if (OnGetValue == null) return;
        OnGetValue[id] -= callback;
    }

    public void TriggerSetEvent(string id, string value) {
        OnSetValue[id]?.Invoke(value);
    }

    public string TriggerGetEvent(string id) {
        if (!OnGetValue.TryGetValue(id, out var cb)) {
            Debug.LogError($"[PauseSetting] {id} 이벤트 값을 가져올 수 없습니다.");
            return string.Empty;
        }

        return OnGetValue[id]?.Invoke();
    }
}
