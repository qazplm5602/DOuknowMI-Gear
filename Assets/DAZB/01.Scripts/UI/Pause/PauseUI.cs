using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject UI;
    [SerializeField] private int selectIndex;
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private RectTransform textsParent;
    [SerializeField] private Gradient gradient;
    private bool isPause;
    private bool isWheel; // true: up, false: down
    private bool isUpdating;
    private void Awake() {
        inputReader.PauseEvent += HandlePauseEvent;
        inputReader.PauseWheelEvent += HandlePauseWheelEvent;
        isPause = false;
        isUpdating = false;
    }

    private void Start() {
        Select(selectIndex);
    }
    
    private void Update() {
        for (int i = 0; i < texts.Length; ++i) {
            if (i == selectIndex) continue;
            Deselect(i);
        }
    }

/*     public void PointerEnter(int index) {
        Select(index);
    }

    public void PointerExit(int index) {
        Deselect(index);
    } */

    private void HandlePauseEvent() {
        UI.SetActive(!isPause);
    }

    private void HandlePauseWheelEvent(float value) {
        if (value == 1) { // wheel up
            if (selectIndex > 0) {
                selectIndex--;
                isWheel = true;
                UpdatePauseUI(isWheel);
            }
        }
        else if (value == -1) { // wheel down
            if (selectIndex < texts.Length - 1) {
                selectIndex++;
                isWheel = false;
                UpdatePauseUI(isWheel);
            }
        };
    }
    
    private void UpdatePauseUI(bool val) {
        StartCoroutine(Select(selectIndex, val));
    }

    private IEnumerator Select(int index, bool val) {
        yield return new WaitUntil(() => isUpdating == false);
        RectTransform rt = texts[index].GetComponent<RectTransform>();
        isUpdating = true;
        texts[index].color = gradient.Evaluate(1);
        rt.DOScale(1.5f, 0.2f);
        if (val) {
            textsParent.DOAnchorPosY(textsParent.localPosition.y - 80, 0f).OnComplete(() => {
                isUpdating = false;
            });
        }
        else {
            textsParent.DOAnchorPosY(textsParent.localPosition.y + 80, 0f).OnComplete(() => {
                isUpdating = false;
            });
        }
    }

    private void Deselect(int index) {
        float distance = Mathf.Abs(index - selectIndex) + 1;
        texts[index].GetComponent<RectTransform>().DOScale(1, 0f);
        texts[index].color = gradient.Evaluate(1 / distance);
    }

    private void Select(int index) {
        RectTransform rt = texts[index].GetComponent<RectTransform>();
        rt.DOScale(1.5f, 0.2f);
    }
}
