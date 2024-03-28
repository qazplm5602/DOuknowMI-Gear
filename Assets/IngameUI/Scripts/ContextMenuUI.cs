using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct ContextButtonDTO {
    public string name;
    public System.Action callback;
}

public class ContextMenuUI : MonoBehaviour
{
    [SerializeField] GameObject buttonTemplate;

    public void OpenMenu(ContextButtonDTO[] menus) {
        // 메뉴 초기화
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        
        foreach (var item in menus)
        {
            var button = Instantiate(buttonTemplate, transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            button.GetComponent<Button>().onClick.AddListener(() => item.callback.Invoke());
        }

        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
