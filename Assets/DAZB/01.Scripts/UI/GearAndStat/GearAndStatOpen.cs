using UnityEngine;
using DG.Tweening;

public class GearAndStatOpen : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject gearPanel;
    [SerializeField] private GameObject statPanel;
    private bool isOpen;

    private void Awake() {
        inputReader.StatOpenEvent += Open;
    }

    private void Open() {
        RectTransform gearRt = gearPanel.GetComponent<RectTransform>();
        RectTransform statRt = statPanel.GetComponent<RectTransform>();
        if (isOpen) {
            isOpen = false;
            gearRt.DOAnchorPosX(-1600, 0.25f).SetEase(Ease.InOutBack);
            statRt.DOAnchorPosX(1300, 0.25f).SetEase(Ease.InOutBack);
            return;
        }
        isOpen = true;
        gearRt.DOAnchorPosX(-400, 0.25f).SetEase(Ease.InOutBack);
        statRt.DOAnchorPosX(700, 0.25f).SetEase(Ease.InOutBack);
    }
}
