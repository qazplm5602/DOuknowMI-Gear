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
    [SerializeField] private string interactionName;
    private bool isCheck;
    private Vector2 pos; // screen Pos
    private TMP_Text excuseMeText;

    private void Awake() {
        excuseMeText = DialogueManager.Instance.ExcuseMeUI.GetComponentInChildren<TMP_Text>(false); 
    }

    private void Update() {
        CheckPlayer();
        if (isCheck) {
            if (excuseMeText != null) {
                excuseMeText.text = interactionName;
            }
            pos = Camera.main.WorldToScreenPoint(excuseMeUiPos.position);
            DialogueManager.Instance.checkInteractiveObejct = true;
            DialogueManager.Instance.ExcuseMeUI.SetActive(true);
            DialogueManager.Instance.ExcuseMeUI.transform.position = pos;
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                Interaction();
            }
        }
        else {
            if (DialogueManager.Instance.npc != null) {
                DialogueManager.Instance.checkInteractiveObejct = false;
            }
            else {
                DialogueManager.Instance.checkInteractiveObejct = false;
                DialogueManager.Instance.ExcuseMeUI.SetActive(false);
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
