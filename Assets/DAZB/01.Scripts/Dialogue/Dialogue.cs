using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private NpcData npcData;
    [SerializeField] private TextAsset dialogueDataFile;
    [SerializeField] private List<DialogueData> greetingList = new();
    [SerializeField] private List<DialogueData> cancleList = new();
    [SerializeField] private List<DialogueData> conversationList = new();
    [SerializeField] private List<DialogueData> interactionList = new();

    private void Awake()
    {
        string data = dialogueDataFile.text;
        List<DialogueData> datas = CSVParser.parse(data);
        foreach(var iter in datas) {
            if (iter.Type == "Cancle") {
                cancleList.Add(iter);
            }
            else if (iter.Type == "Conversation") {
                conversationList.Add(iter);
            }
            else if (iter.Type == "Greeting") {
                greetingList.Add(iter);
            }
            else if (iter.Type == "Interaction") {
                interactionList.Add(iter);
            }
        }
    }
/* 
    private void Start() {
        Conversation();
    }

    public void Greeting() {
        StartCoroutine(GreetingRoutine());
    }

    public void Cancle() {
        StartCoroutine(CancleRoutine());
    }

    public void Conversation() {
        StartCoroutine(ConversationRoutine());
    }

    public void Interaction() {
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator GreetingRoutine() {
        int randNum = Random.Range(1, int.Parse(greetingList[greetingList.Count - 1].RandomType));
        foreach (var iter in greetingList) {
            if (int.Parse(iter.RandomType) == randNum) {
                DialogueManager.instance.SetDialogue(npcData.Name, iter.Content, npcData.InteractionName);
                yield return new WaitUntil(() => true == DialogueManager.instance.isEnd);
            }
        }
        yield return null;
    }

    private IEnumerator ConversationRoutine() {
        int randNum = Random.Range(1, int.Parse(conversationList[conversationList.Count - 1].RandomType));
        foreach (var iter in conversationList) {
            if (int.Parse(iter.RandomType) == randNum) {
                DialogueManager.instance.SetDialogue(npcData.Name, iter.Content, npcData.InteractionName);
                yield return new WaitUntil(() => true == DialogueManager.instance.isEnd);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        yield return null;
    }

    private IEnumerator InteractionRoutine() {
        yield return null;
    }

    private IEnumerator CancleRoutine() {
        yield return null;
    } */
}
