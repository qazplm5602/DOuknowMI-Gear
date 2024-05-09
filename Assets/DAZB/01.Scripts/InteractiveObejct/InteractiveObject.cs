using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InteractiveObject : MonoBehaviour, IInteraction
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector3 checkSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform excuseMeUiPos;
    [SerializeField] private Transform nameTagPos;
    [SerializeField] private string objectName;
    public   string interactionName;
    private bool isCheck;
    private Vector2 ecUiPos;
    private Vector2 nTPos;
    private TMP_Text excuseMeText;
    private TMP_Text nameTagText;

    protected virtual void Awake() {
        excuseMeText = DialogueManager.Instance.ExcuseMeUI.GetComponentInChildren<TMP_Text>(false);
        nameTagText = DialogueManager.Instance.NameTag.GetComponentInChildren<TMP_Text>(false);
    }

    private void Update() {
        CheckPlayer();
        if (isCheck) {
            if (excuseMeText != null) {
                excuseMeText.text = interactionName;
            }
            ecUiPos = Camera.main.WorldToScreenPoint(excuseMeUiPos.position);
            nTPos = Camera.main.WorldToScreenPoint(nameTagPos.position);
            DialogueManager.Instance.checkInteractiveObejct = true;
            DialogueManager.Instance.ExcuseMeUI.SetActive(true);
            DialogueManager.Instance.ExcuseMeUI.transform.position = ecUiPos;
            nameTagText.text= objectName;
            DialogueManager.Instance.NameTag.SetActive(true);
            DialogueManager.Instance.NameTag.transform.position = nTPos;
            DialogueManager.Instance.nowInteractiveObjectName = gameObject.name;
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                Interaction();
            }
        }
        else {
            if (DialogueManager.Instance.npc != null) {
                DialogueManager.Instance.checkInteractiveObejct = false;
            }
            else {
                if (DialogueManager.Instance.nowInteractiveObjectName == gameObject.name) {
                    DialogueManager.Instance.checkInteractiveObejct = false;
                    DialogueManager.Instance.ExcuseMeUI.SetActive(false);
                    DialogueManager.Instance.NameTag.SetActive(false);
                }
            }
        }
    }


    private void CheckPlayer() {
        isCheck = Physics2D.OverlapBox(transform.position + offset, checkSize, 0, playerLayer);
    }

    protected virtual void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, checkSize);
    }

    public abstract void Interaction();
}
