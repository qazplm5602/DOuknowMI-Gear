using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    /////////////// 스태틱
    static string goScene = "Stage";
    
    public static void LoadScene(string scene) {
        goScene = scene;

        // 로딩 씬으로 ㄱㄱ
        SceneManager.LoadScene("Loading");
    }

    /////////////// OBJ
    [SerializeField] LoadGearSpinner[] spinners;
    [SerializeField] RectTransform character;
    [SerializeField] TextMeshProUGUI progressT;
    [SerializeField] Transform bar;
    [SerializeField] AnimationCurve _curve;

    float screenWidth;

    private void Awake() {
        screenWidth = bar.parent.GetComponent<RectTransform>().rect.width;
    }

    private void Start() {
        StartCoroutine(StartLoadScene());
    }

    class FakeAsync {
        public float progress;
        public bool allowSceneActivation;
        public bool isDone;
    }

    IEnumerator StartLoadScene() {
        AsyncOperation data = SceneManager.LoadSceneAsync(goScene);
        // FakeAsync data = new();
        data.allowSceneActivation = false;
        
        float lastProgress;
        float progress = 0;

        float testTime = Time.time;

        float curveTime = 0;
        lastProgress = 0;
        while (curveTime < 1) {
            yield return null;
            curveTime += Time.deltaTime / 8;
            progress = Mathf.Clamp(_curve.Evaluate(curveTime), 0, 0.9f);
            
            UpdateUI(progress);
        }
        
        lastProgress = data.progress;
        // while (!data.isDone && progress < 0.9f) {
        while (!data.isDone && data.progress < 0.9f) {
            yield return null;

            if (progress > data.progress) {
                yield return new WaitUntil(() => data.progress < progress);
            }
            // if (Time.time - testTime > 0.05f) {
            //     testTime = Time.time;
            //     data.progress += 0.01f;
            // }

            
            progress = Mathf.Lerp(progress, data.progress, Time.deltaTime * 5);
            // progress = _curve.Evaluate(progress);
            // print($"{_curve.Evaluate(0)} / {_curve.Evaluate(0.5f)} / {_curve.Evaluate(1)}");
            
            if (Mathf.Abs(progress - data.progress) < 0.001f) {
                progress = data.progress;
            }

            if (Mathf.Abs(lastProgress - progress) < 0.01f) {
                UpdateUI(progress);
            }

            lastProgress = progress;
        }

        UpdateUI(data.progress);
        data.allowSceneActivation = true;
    }

    void UpdateUI(float progress) {
        Vector2 pos = character.anchoredPosition;
        pos.x = screenWidth * progress;
        character.anchoredPosition = pos;

        progressT.text = $"{Mathf.FloorToInt(progress * 100)}%";
        bar.localScale = new(progress, 1, 1);

        foreach (var item in spinners)
            item.Set(progress);
    }
}
