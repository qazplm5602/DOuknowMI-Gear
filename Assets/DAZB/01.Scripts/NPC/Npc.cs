using UnityEngine;

public abstract class Npc : MonoBehaviour, IInteraction
{
    [SerializeField] private NpcData npcData;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 checkBoxSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject excuseMeUI;
    protected bool isCheck;
    private Dialogue dialogue;

    public NpcData GetNpcData() => npcData;
    public abstract void Interaction();
    
    private void Awake() {
        dialogue = GetComponent<Dialogue>();
    }

    private void Start() {
        ExcuseMe();
    }

    private void Update() {
        CheckPlayer();
        if (isCheck) {

        }
        else {

        }
    }

    public void ExcuseMe() {
        dialogue.StartDialogue();
        DialogueManager.instance.ActiveDialoguePanel(true);
        DialogueManager.instance.Conversation();
    }

    private void CheckPlayer() {
        isCheck = Physics2D.OverlapBox(transform.position + offset, checkBoxSize, 0, whatIsPlayer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, checkBoxSize);
    }
}
