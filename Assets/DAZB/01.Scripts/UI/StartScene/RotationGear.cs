using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;

public class RotationGear : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject gear;
    [SerializeField] private Image panel;
    [SerializeField] private GameObject _storyCutScene;
    private bool isRotation;
    private bool isDragging;
    private bool isStart;
    private int check;

    public void SetIsRotation(bool value)
    {
        isRotation = value;
    }

    public void ResetValue() {
        isStart = false;
        gear.transform.DORotate(Vector3.zero, 0.2f).SetEase(Ease.Linear);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isStart) return;
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging == false || isRotation == false) return;
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - gear.transform.position;
        float rotationAmount = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
        gear.transform.eulerAngles = new Vector3(0, 0, rotationAmount - 90);
        if (gear.transform.eulerAngles.z >= 175f && gear.transform.eulerAngles.z <= 185f) {
            isStart = true;
            isDragging = false;
            gear.transform.eulerAngles = new Vector3(0, 0, 180);
            StartCoroutine(GameStartRoutine());
        }
    }

    private IEnumerator GameStartRoutine() {
        yield return new WaitForSeconds(0.5f);
        panel.DOFade(1, 1f);
        yield return new WaitForSeconds(1.2f);
        GameObject obj = Instantiate(_storyCutScene);
        obj.transform.GetChild(0).GetComponent<StoryTimeline>().OnComplete += () => TitleManager.Instance.ChangeSceneToVillage();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        if (isStart == true) return; 
        gear.transform.DORotate(Vector3.zero, 0.2f).SetEase(Ease.Linear);
    }
}