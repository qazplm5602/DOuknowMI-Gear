using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;   

public class StartSceneMenu : MonoBehaviour
{
    [SerializeField] private GameObject exitUI;
    [SerializeField] private GameObject newGameUI;
    [SerializeField] private GameObject menuObj;
    private bool isExitUiOpen = false;
    private CanvasGroup menuObjCanvasGroup;
    private CanvasGroup newGameUiCanvasGroup;
    private Button menuObjButton;
    private GameObject newSelectButton;

    private void Awake() {
        menuObjCanvasGroup = menuObj.GetComponent<CanvasGroup>();
        newGameUiCanvasGroup = newGameUI.GetComponent<CanvasGroup>();
        menuObjButton = menuObj.GetComponentInChildren<Button>();
    }

    public void NewGame() {
        if (isExitUiOpen) return;
        newSelectButton.transform.DOScale(1, 0.2f);
        StartCoroutine(NewGameRoutine());
    }

    private IEnumerator NewGameRoutine() {
        newGameUI.SetActive(true);
        menuObjButton.interactable = false;
        menuObjCanvasGroup.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        newGameUiCanvasGroup.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        menuObjButton.interactable = true;
    }

    public void Setting() {
        if (isExitUiOpen) return;
        newSelectButton.transform.DOScale(1, 0.2f);
    }

    public void LoadSave() {
        if (isExitUiOpen) return;
        newSelectButton.transform.DOScale(1, 0.2f);
    }

    public void Exit() {
        if (isExitUiOpen) return;
        newSelectButton.transform.DOScale(1, 0.2f);
        isExitUiOpen = true;
        exitUI.SetActive(true);
    }

    public void Close(string type) {
        if (type == "NewGame") {
            StartCoroutine(NewGameCloseRoutine());
        }
        else if (type == "LoadGame") {

        }
        else if (type == "Setting") {

        }
        else if (type == "Exit" ) {
            isExitUiOpen = false;
            exitUI.SetActive(false);
        }
    }

    private IEnumerator NewGameCloseRoutine() {
        newGameUiCanvasGroup.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        newGameUI.SetActive(false);
        menuObjCanvasGroup.DOFade(1, 0.2f);
    }

    public void Confirm() {
        Application.Quit();
    }
    
    public void MouseEnter(GameObject obj) {
        if (isExitUiOpen) return;
        newSelectButton = obj;
        obj.transform.DOScale(1.2f, 0.1f);
    }
    public void MouseExit(GameObject obj) {
        if (isExitUiOpen) return;
        newSelectButton = obj;
        obj.transform.DOScale(1f, 0.1f);
    }
}
