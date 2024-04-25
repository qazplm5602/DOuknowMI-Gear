using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPauseButtonTooltip : MonoBehaviour
{
    IEnumerator process;
    CanvasGroup _group;
    RectTransform rectTrm;

    bool showing = false;

    private void Awake() {
        rectTrm = transform as RectTransform;
        _group = GetComponent<CanvasGroup>();
    }

    public void SetToggle(bool value) {
        print($"SetToggle {value}");
        showing = value;
        
        if (process == null) {
            process = SmoothAnim();
            StartCoroutine(process);
        }
    }

    // 강제로 숨김
    public void ForceHide() {
        if (process != null) {
            StopCoroutine(process);
            process = null;
        }

        showing = false;
        _group.alpha = 0;
        rectTrm.anchoredPosition = new(0, rectTrm.anchoredPosition.y);
    }

    IEnumerator SmoothAnim() {
        while (true) {
            float ming = Time.unscaledDeltaTime * 20;

            rectTrm.anchoredPosition = Vector2.Lerp(rectTrm.anchoredPosition, new(showing ? 10 : 0, rectTrm.anchoredPosition.y), ming);
            _group.alpha = Mathf.Lerp(_group.alpha, showing ? 1 : 0, ming);

            if ( (showing == true && _group.alpha >= .95f && rectTrm.anchoredPosition.x >= 9.5f) || (showing == false && _group.alpha <= .05f && rectTrm.anchoredPosition.x <= 0.5f) ) {
                rectTrm.anchoredPosition = new(showing ? 10 : 0, rectTrm.anchoredPosition.y);
                _group.alpha = showing ? 1 : 0;
                break;
            }

            yield return null;
        }

        process = null;
    }
}
