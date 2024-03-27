using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextAsset dialogueDataFile;
    public List<DialogueData> greetingList = new();
    public List<DialogueData> cancleList = new();
    public List<DialogueData> conversationList = new();
    public List<DialogueData> interactionList = new();

    private void Start()
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
}
