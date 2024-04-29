using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIControl : MonoSingleton<IngameUIControl>
{
    ///////////// Config
    [SerializeField] GameObject _healthBar;
    [SerializeField] GameObject _location;
    [SerializeField] RectTransform _coinBox;

    [Header("Scripts")]
    [SerializeField] GearChangeUI _gearChangeUI;
    [SerializeField] GearPauseUI _gearPauseUI;
    [SerializeField] StatueUI _statueUI;
    public GearChangeUI gearChangeUI => _gearChangeUI;

    TextMeshProUGUI locationT;
    Image healthRed;
    Image healthGreen;
    TextMeshProUGUI healthLevel;
    CanvasGroup coinGroup;
    TextMeshProUGUI coinT;

    private void Awake() {
        locationT = _location.GetComponentInChildren<TextMeshProUGUI>();
        
        healthRed = _healthBar.transform.Find("Red_in").GetComponent<Image>();
        healthGreen = _healthBar.transform.Find("Green_in").GetComponent<Image>();
        healthLevel = _healthBar.transform.Find("Level").GetComponent<TextMeshProUGUI>();
        coinGroup = _coinBox.GetComponent<CanvasGroup>();
        coinT = _coinBox.Find("value").GetComponent<TextMeshProUGUI>();

        // TEST
        SetHealthBar(50, 100);
        SetHealthLevelBar(50, 100);

        SetLocation("도미시티");
        SetHealthLevel(53);

        // SetCoin(1000);
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

    int coinState = 0; // 0: 아무것도 아님 / 1: In 애니메이션 중 2: 애니메이션 나가는중
    Sequence coinSequence;
    IEnumerator coinCoroutine;
    
    public void SetCoin(int value) {
        if (coinSequence != null && coinState != 1) {
            coinSequence.Kill();
            coinSequence = null;
        }
        
        if (coinSequence == null) {
            coinState = 1;
            coinSequence = DOTween.Sequence();
            coinSequence.Append(coinGroup.DOFade(1, 0.3f).SetEase(Ease.OutQuad));
            coinSequence.Join(_coinBox.DOAnchorPosX(30, 0.3f).SetEase(Ease.OutQuad));
            coinSequence.OnComplete(() => {
                coinState = 0;
                coinSequence = null;
            });
        }

        if (coinCoroutine != null)
            StopCoroutine(coinCoroutine);

        coinCoroutine = CoinCounting(value);
        StartCoroutine(coinCoroutine);
    }

    IEnumerator CoinCounting(int value) {
        yield return new WaitUntil(() => coinState == 0);

        int nowVal = int.Parse(coinT.text.Replace(@",", ""));
        int diff = Mathf.Abs(value - nowVal);

        float nowT = 0;

        while (nowT < 1) {
            nowT += Time.deltaTime / 2;
            nowVal = (int)Mathf.Lerp(nowVal, value, nowT);

            coinT.text = nowVal.ToString("N0");
            yield return null;
        }

        yield return new WaitForSecondsRealtime(3f);

        coinState = 2;

        coinSequence = DOTween.Sequence();
        coinSequence.Append(coinGroup.DOFade(0, 0.3f).SetEase(Ease.OutQuad));
        coinSequence.Join(_coinBox.DOAnchorPosX(0, 0.3f).SetEase(Ease.OutQuad));
        coinSequence.OnComplete(() => {
            coinState = 0;
            coinSequence = null;
        });

        coinCoroutine = null;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            _statueUI.Show();
        } else if (Input.GetKeyDown(KeyCode.O)) {
            _statueUI.Hide();
        }
    }
}
