using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GuideMenu {
    public string title;
    public string sub;
    public GameObject content;
}

public class GuideUI : MonoBehaviour
{
    [SerializeField] GuideBoxUI _boxPrefab;
    [SerializeField] Transform _menuSection;
    [SerializeField] Transform _viewport;
    [SerializeField] GuideMenu[] _menus;

    ScrollRect scrollRect;
    
    private void Awake() {
        foreach(var item in _menus) {
            GuideBoxUI box = Instantiate(_boxPrefab, _menuSection);
            box.Init(item, this);
        }

        scrollRect = _viewport.parent.GetComponent<ScrollRect>();
    }

    public void Show() {

    }

    public void ShowContent(GameObject content)
    {
        if (_viewport.childCount > 0)
            Destroy(_viewport.GetChild(0).gameObject);

        scrollRect.content = Instantiate(content, _viewport).transform as RectTransform;
    }
}
