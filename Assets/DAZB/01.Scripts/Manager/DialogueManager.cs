using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public float charPrintTime;
    public GameObject DialoguePanel;
    public GameObject SelectionPanel;
    public TMP_Text nameText;
    public TMP_Text contentText;
    public Button ConversationBtn;
    public Button InteractionBtn;
    public Button CancleBtn;
    [Tooltip("대사가 끝나면 true, 시작하면 false")]
    public bool isEnd;
    private Tween tween;

    private void Awake() {
        instance = this;
    }

    public void SetDialogue(string npcName, string content, string interactionName) {
        StartCoroutine(SetDialogueRoutine(npcName, content, interactionName));
    }

    private IEnumerator SetDialogueRoutine(string npcName, string content, string interactionName) {
        isEnd = false;
        nameText.text = npcName;
        contentText.text = content;
        InteractionBtn.GetComponentInChildren<TMP_Text>().text = interactionName;
        TypeText(contentText);
        tween.OnComplete(() => {
            isEnd = true;
            return;
        });
        yield return new WaitUntil(() =>Input.GetKeyDown(KeyCode.Space));
        SkipTween();
    }

    public void TypeText(TMP_Text text) {
        text.maxVisibleCharacters = 0;
        tween = DOTween.To(
            x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, text.text.Length * charPrintTime
        ).SetEase(Ease.Linear);
    }

    public void SkipTween() {
        tween.Complete();
        print("실행");
    }
}
