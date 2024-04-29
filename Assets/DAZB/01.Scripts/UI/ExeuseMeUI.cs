using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeuseMeUI : MonoBehaviour
{
    [SerializeField] private RectTransform FBox;
    [SerializeField] private RectTransform text; // Text에 자식을 넣어야함

    private void Update() {
        // FBox의 위치를 text 옆에 붙이도록 조절
        FBox.anchoredPosition = new Vector2(text.anchoredPosition.x - text.sizeDelta.x / 2 + FBox.sizeDelta.x / 2 - 40, FBox.anchoredPosition.y);
    }
}
