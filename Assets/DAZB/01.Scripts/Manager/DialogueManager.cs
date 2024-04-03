using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public InputReader inputReader;
    public float charPrintTime;
    public GameObject DialoguePanel;
    public GameObject SelectionPanel;
    public TMP_Text nameText;
    public TMP_Text contentText;
    public Button ConversationBtn;
    public Button InteractionBtn;
    public Button CancleBtn;
    public List<DialogueData> greetingList = new();
    public List<DialogueData> cancleList = new();
    public List<DialogueData> conversationList = new();
    public List<DialogueData> interactionList = new();
    public Npc npc;
    [Tooltip("대사가 끝나면 true, 시작하면 false")]
    public bool isEnd = true;
    private Tween tween;

    private void Awake() {
        instance = this;
    }

    public void Init(List<DialogueData> greetingList, List<DialogueData> cancleList,
        List<DialogueData> conversationList, List<DialogueData> interactionList, Npc npc) {
            this.greetingList = greetingList;
            this.cancleList = cancleList;
            this.conversationList = conversationList;
            this.interactionList = interactionList;
            this.npc = npc;
    }

    public void ActiveDialoguePanel(bool isActive) {
        DialoguePanel.SetActive(isActive);
        PlayerManager.instance.player.enabled = !!!isActive;
    }

    public void ActiveSelectionPanel(bool isActive) {
        SelectionPanel.SetActive(isActive);
    }

    public void SetDialogue(string npcName, string content, string interactionName) {
        StartCoroutine(SetDialogueRoutine(npcName, content, interactionName));
    }

    public void Greeting() {
        StartCoroutine(GreetingRoutine());
    }

    public void Conversation() {
        StartCoroutine(ConversationRoutine());
    }

    public void Cancle() {
        StartCoroutine(CancleRoutine());
    }

    public void Interaction() {
        //npc.Interaction();
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator GreetingRoutine() {
        int randNum = Random.Range(1, int.Parse(greetingList[greetingList.Count - 1].RandomType));
        foreach (DialogueData data in greetingList) {
            if (int.Parse(data.RandomType )== randNum) {
                yield return StartCoroutine(SetDialogueRoutine(npc.GetNpcData().Name, data.Content, npc.GetNpcData().InteractionName));
                yield return new WaitUntil(() =>Input.GetKeyDown(KeyCode.Space));
            }
        }
        yield return null;
    }

    private IEnumerator ConversationRoutine() {
        int randNum = Random.Range(1, int.Parse(conversationList[conversationList.Count - 1].RandomType));
        foreach (DialogueData data in conversationList) {
            if (int.Parse(data.RandomType )== randNum) {
                yield return StartCoroutine(SetDialogueRoutine(npc.GetNpcData().Name, data.Content, npc.GetNpcData().InteractionName));
                yield return new WaitUntil(() =>Input.GetKeyDown(KeyCode.Space));
            }
        }
        yield return null;
    }

    private IEnumerator InteractionRoutine() {
        yield return null;
    }

    private IEnumerator CancleRoutine() {
        yield return null;
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
        print("대사 스킵");
    }
}
