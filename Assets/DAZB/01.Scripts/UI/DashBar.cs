using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public static DashBar instance;
    [SerializeField] private Image dashCharge;
    private int nowCharge;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        nowCharge = PlayerManager.instance.stats.maxDashCount;
    }

    public void DecreaseDashCount() {
        RectTransform rt = dashCharge.GetComponent<RectTransform>();
        rt.localScale = new Vector2(rt.localScale.x - 0.25f, 1);
    }

    public void IncreaseDashCount() {
        RectTransform rt = dashCharge.GetComponent<RectTransform>();
        rt.localScale = new Vector2(rt.localScale.x + 0.25f, 1);
    }
}
