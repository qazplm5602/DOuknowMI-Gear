using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIControl : MonoBehaviour
{
    ///////////// Config
    [SerializeField] GameObject _healthBar;
    [SerializeField] GameObject _location;

    TextMeshProUGUI locationT;
    Image healthRed;
    Image healthGreen;
    TextMeshProUGUI healthLevel;

    private void Awake() {
        locationT = _location.GetComponentInChildren<TextMeshProUGUI>();
        
        healthRed = _healthBar.transform.Find("Red_in").GetComponent<Image>();
        healthGreen = _healthBar.transform.Find("Green_in").GetComponent<Image>();
        healthLevel = _healthBar.transform.Find("Level").GetComponent<TextMeshProUGUI>();

        // TEST
        SetHealthBar(50, 100);
        SetHealthLevelBar(50, 100);

        SetLocation("도미시티");
        SetHealthLevel(53);
    }

    public void SetHealthBar(float current, float max) {
        healthRed.fillAmount = current / max;
    }

    public void SetHealthLevelBar(float current, float max) {
        healthGreen.fillAmount = current / max;
    }

    public void SetLocation(string location) {
        locationT.text = location;
    }

    public void SetHealthLevel(int level) {
        healthLevel.text = level.ToString();
    }
}
