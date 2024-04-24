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

    IEnumerator StartLoadScene() {
        AsyncOperation data = SceneManager.LoadSceneAsync(goScene);
        data.allowSceneActivation = false;
        
        float lastProgress = data.progress;

        print(data.progress);
        while (!data.isDone && data.progress < 0.9f) {
            if (lastProgress != data.progress) {
                lastProgress = data.progress;
                UpdateUI(data.progress);
            }
            yield return null;
        }

        UpdateUI(data.progress);
    }

    void UpdateUI(float progress) {
        Vector2 pos = character.anchoredPosition;
        pos.x = screenWidth * progress;
        character.anchoredPosition = pos;

        progressT.text = $"{Mathf.FloorToInt(progress * 100)}%";
        bar.localScale = new(progress, 1, 1);

        foreach (var item in spinners)
            item.Set(progress * 10);
    }
}
