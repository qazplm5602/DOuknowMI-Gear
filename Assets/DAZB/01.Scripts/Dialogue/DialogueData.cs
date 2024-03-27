[System.Serializable]
public class DialogueData {
    public string speaker;
    public string content;
    public DialogueData(string speaker, string content) {
        this.speaker = speaker;
        this.content = content;
    }
}