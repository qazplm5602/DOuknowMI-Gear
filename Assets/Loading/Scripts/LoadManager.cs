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
        // AsyncOperation data = SceneManager.LoadSceneAsync(goScene);
        FakeAsync data = new();
        data.allowSceneActivation = false;
        
        float lastProgress = data.progress;
        float progress = 0;

        float testTime = Time.time;
        
        while (!data.isDone && progress < 0.9f) {
            yield return null;
            if (Time.time - testTime > Random.Range(.5f, 6)) {
                testTime = Time.time;
                data.progress += 0.1f;
            }
            
            progress = Mathf.Lerp(progress, data.progress, Time.deltaTime * 5);
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
