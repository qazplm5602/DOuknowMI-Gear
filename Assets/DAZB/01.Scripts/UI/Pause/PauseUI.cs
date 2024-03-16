using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Text[] texts;

    private void Awake() {
        texts = GetComponentsInChildren<Text>();
    }

    public void PointerEnter(int index) {
        Select(index);
    }

    public void PointerExit() {
        Deselect();
    }

    private void Select(int index) {
        
    }

    private void Deselect() {

    }
}
