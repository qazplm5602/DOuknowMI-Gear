using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private RectTransform maskObject;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float[] durations; // 0번이 처음 1번이 두번째, 2번이 텍스트 나오는 시간
    private RectTransform textTranform;

    private void Start() {
        PlayerManager.instance.player.HealthCompo.OnDead += HadleDeadEvent;
    }

    private void OnDisable() {
        PlayerManager.instance.player.HealthCompo.OnDead -= HadleDeadEvent;
    }

    private void HadleDeadEvent()
    {
        StartCoroutine(DeadRoutine());
    }

    private IEnumerator DeadRoutine() {
        DOTween.To(size => maskObject.sizeDelta = new Vector2(size, size), 4000, 500, durations[0]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(durations[0]);
        text.gameObject.SetActive(true);
        DOTween.To(alpha => text.color = new Color(1, 1, 1,alpha), 0, 1, durations[2]);
        yield return new WaitForSeconds(durations[2]);

        yield return new WaitUntil(() => Keyboard.current.anyKey.wasPressedThisFrame);
        DOTween.To(alpha => text.color = new Color(1, 1, 1,alpha), 1, 0, durations[2]);
        yield return new WaitForSeconds(durations[2]);
        DOTween.To(size => maskObject.sizeDelta = new Vector2(size, size), 500, 0, durations[1]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(durations[1]);
        SceneManager.LoadScene("Village");
        yield return null;
    }
}
