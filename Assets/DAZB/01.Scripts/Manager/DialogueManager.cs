using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManager : MonoSingleton<DialogueManager>
{    public InputReader inputReader;
    public GameObject ExcuseMeUI;
    public GameObject DotTwinkleUi;
    public GameObject NameTag;
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
    public Queue<DialogueData> selectSentence = new();
    public Npc npc;
    [Tooltip("대사가 끝나면 true, 시작하면 false")]
    public bool isEnd = true;
    public bool checkInteractiveObejct;
    [HideInInspector] public string nowInteractiveObjectName;
    private TMP_Text interactionText;
/*     private IEnumerator _dialogue; */
    private void Awake() {
        if (InteractionBtn != null)
            interactionText = InteractionBtn.GetComponentInChildren<TMP_Text>();
    }

    public void Init(List<DialogueData> greetingList, List<DialogueData> cancleList,
        List<DialogueData> conversationList, List<DialogueData> interactionList) {
            this.greetingList = greetingList;
            this.cancleList = cancleList;
            this.conversationList = conversationList;
            this.interactionList = interactionList;
    }

    private void SetSentence(List<DialogueData> list, int randNum) {
        print(randNum);
        for (int i = 0; i < list.Count; ++i) {
            if (list[i].RandomType == randNum.ToString()) {
                selectSentence.Enqueue(list[i]);
                print(list[i].Content);
            }
        }
    }

    public void SetNpc(Npc npc) {
        this.npc = npc;
        nameText.text = npc.GetNpcData().Name;
        interactionText.text = npc.GetNpcData().NpcInteractionName;
        ConversationBtn.gameObject.SetActive(npc.GetNpcData().CanConversate);
        InteractionBtn.gameObject.SetActive(npc.GetNpcData().CanInteract);
    }

    public void ActiveDialoguePanel(bool isActive) {
        DialoguePanel.SetActive(isActive);
        PlayerManager.instance.player.enabled = !!!isActive;
        //isEnd = !!!isActive;
        npc.SetIsDialogue(isActive);
    }

    public void SetEnd(bool value) {
        isEnd = value;
    }

    public void ActiveSelectionPanel(bool isActive) {
        SelectionPanel.SetActive(isActive);
    }

/*     public void SetDialogue(string npcName, string content, string interactionName) {
        StartCoroutine(SetDialogueRoutine(npcName, content, interactionName));
    } */

    public void Greeting() {
        StartCoroutine(GreetingRoutine());
    }

    public void Conversation() {
        print("인");
        StartCoroutine(ConversationRoutine());
    }

    public void Cancle() {
        print("캔");
        StartCoroutine(CancleRoutine());
    }

    public void Interaction() {
        //npc.Interaction();
        print("상");
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator GreetingRoutine() {
        SetEnd(false);
        PlayerManager.instance.player.StateMachine.ChangeState(PlayerStateEnum.Interaction);
        int randNum = Random.Range(1, int.Parse(greetingList[greetingList.Count - 1].RandomType) + 1);
        SetSentence(greetingList, randNum);
        ActiveSelectionPanel(false);
        DialogueData sentence;
        while (true) {
            if (selectSentence.Count == 0) {
                break;
            }
            sentence = selectSentence.Dequeue();
            contentText.text = sentence.Content;
            yield return StartCoroutine(TypeText(contentText));
            if (selectSentence.Count == 0) {
                ActiveSelectionPanel(true);
                break;
            }
            yield return StartCoroutine(DotTwinkle());
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    private IEnumerator ConversationRoutine() {
        int randNum = Random.Range(1, int.Parse(conversationList[conversationList.Count - 1].RandomType) + 1);
        SetSentence(conversationList, randNum);
        ActiveSelectionPanel(false);
        DialogueData sentence;
        while (true) {
            sentence = selectSentence.Dequeue();
            contentText.text = sentence.Content;
            yield return StartCoroutine(TypeText(contentText));
            if (selectSentence.Count == 0) {
                ActiveSelectionPanel(true);
                break;
            }
            yield return StartCoroutine(DotTwinkle());
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    private IEnumerator InteractionRoutine() {
        int randNum = Random.Range(1, int.Parse(interactionList[interactionList.Count - 1].RandomType) + 1);
        SetSentence(interactionList, randNum);
        ActiveSelectionPanel(false);
        DialogueData sentence;
        while (true) {
            if (selectSentence.Count == 0) {
                ActiveSelectionPanel(true);
                break;
            }
            sentence = selectSentence.Dequeue();
            contentText.text = sentence.Content;
            yield return StartCoroutine(TypeText(contentText));
            yield return StartCoroutine(DotTwinkle());
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            yield return new WaitForSeconds(0.2f);
        }
        npc.Interaction();
        ActiveDialoguePanel(false);
        yield return null;
    }

    private IEnumerator CancleRoutine() {
        int randNum = Random.Range(1, int.Parse(cancleList[cancleList.Count - 1].RandomType) + 1);
        SetSentence(cancleList, randNum);
        ActiveSelectionPanel(false);
        DialogueData sentence;
        while (true) {
            if (selectSentence.Count == 0) {
                yield return new WaitForSeconds(1f);
                break;
            }
            sentence = selectSentence.Dequeue();
            contentText.text = sentence.Content;
            yield return StartCoroutine(TypeText(contentText));
            yield return StartCoroutine(DotTwinkle());
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            yield return new WaitForSeconds(0.2f);
        }
        ActiveDialoguePanel(false);
        SetEnd(true);
        yield return null;
    } 

    private IEnumerator TypeText(TMP_Text text) {
        Tween tween;
        text.maxVisibleCharacters = 0;
        tween = DOTween.To(
            x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, text.text.Length * charPrintTime
        ).SetEase(Ease.Linear);
        while (true) {
            if (!tween.IsActive()|| Keyboard.current.spaceKey.wasPressedThisFrame) {
                break;
            }
            yield return null;
        }
        if (tween.IsActive()) {
            SkipTween(tween);
        }
        yield return null;
    }

    private IEnumerator DotTwinkle() {
        float currentTime = 0.0f;
        bool isActive = true;
        while (true) {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                DotTwinkleUi.SetActive(false);
                yield break;
            }
            if (currentTime > 0.5) {
                isActive = !isActive;
                DotTwinkleUi.SetActive(isActive);
                currentTime = 0.0f;
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SkipTween(Tween tween) {
        tween.Complete();
        tween.Kill();
    }
}
