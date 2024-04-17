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
    [Header("Section")]
    [SerializeField] Transform _categorys;
    [SerializeField] Transform _scrollContent;
    [SerializeField] TextMeshProUGUI _description;

    [Header("Prefab")]
    [SerializeField] Button _sectionBtn;
    [SerializeField] GameObject _box;
    
    [Header("Config")]
    [SerializeField] PauseSettingSOPair[] menus;

    private void Awake() {
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


        
    }
}
