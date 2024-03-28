using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text contentText;

    [Header("대화")]
    public TextAsset dialogueDataFile;
    public List<DialogueData> greetingList = new();
    public List<DialogueData> cancleList = new();
    public List<DialogueData> conversationList = new();
    public List<DialogueData> interactionList = new();

    private void Awake()
    {
        string data = dialogueDataFile.text;
        List<DialogueInfo> datas = CSVParser.parse(data);
        foreach(var iter in datas) {
            if (iter.Type == "Cancle") {
                cancleList.Add(new DialogueData(iter.Speaker, iter.Content));
            }
            else if (iter.Type == "Conversation") {
                conversationList.Add(new DialogueData(iter.Speaker, iter.Content));
            }
            else if (iter.Type == "Greeting") {
                greetingList.Add(new DialogueData(iter.Speaker, iter.Content));
            }
            else if (iter.Type == "Interaction") {
                interactionList.Add(new DialogueData(iter.Speaker, iter.Content));
            }
        }
    }

    private void Start() {
        Greeting();
    }

    public void Greeting() {
       /*  nameText.text = greetingList[0].speaker;
        contentText.text = greetingList[0].content; */
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
        yield return null;
    }

    private IEnumerator ConversationRoutine() {
        yield return null;
    }

    private IEnumerator InteractionRoutine() {
        yield return null;
    }

    private IEnumerator CancleRoutine() {
        yield return null;
    }

    private void TypeText(TMP_Text text , float duration) {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
}
