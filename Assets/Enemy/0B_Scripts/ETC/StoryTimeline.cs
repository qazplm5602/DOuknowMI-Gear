using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StoryTimeline : MonoBehaviour
{
    struct Scene {
        public Image image;
        public TextMeshProUGUI[] text;
    }

    public Action OnComplete;

    [SerializeField] private Scene[] scenes = new Scene[10];

    [SerializeField] private float timeBetweenScenes;

    private void Start() {
        int count = transform.childCount;
        for(int i = 0; i < count; ++i) {
            scenes[i].image = transform.GetChild(i).GetComponentInChildren<Image>();
            scenes[i].text = transform.GetChild(i).GetComponentsInChildren<TextMeshProUGUI>();

            if(scenes[i].image != null)
                scenes[i].image.color = new Color(1, 1, 1, 0);
            for(int j = 0; j < scenes[i].text.Length; ++j)
                scenes[i].text[j].color = new Color(1, 1, 1, 0);
        }

        StartScene();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            StopAllCoroutines();
            OnComplete?.Invoke();
        }
    }

    public void StartScene() {
        StartCoroutine(ShowingScene());
    }

    private IEnumerator ShowingScene() {
        WaitForSeconds sec = new WaitForSeconds(timeBetweenScenes);

        for(int i = 0; i < scenes.Length; ++i) {
            scenes[i].image.DOFade(1, 1f);

            for(int j = 0; j < scenes[i].text.Length; ++j) {
                scenes[i].text[j].DOFade(1, 1f);

                //if(j != scenes[i].text.Length - 1)
                    yield return new WaitForSeconds(timeBetweenScenes / 2f);
            }

            yield return sec;

            scenes[i].image.DOFade(0, 0.4f);

            for(int j = 0; j < scenes[i].text.Length; ++j) {
                scenes[i].text[j].DOFade(0, 0.4f);
            }
        }

        OnComplete?.Invoke();
    }
}
