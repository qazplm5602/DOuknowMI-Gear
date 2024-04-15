using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Npc : MonoBehaviour, IInteraction
{
    [SerializeField] private NpcData npcData;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 checkBoxSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform excuseMeUiPos;
    protected bool isCheck;
    private Dialogue dialogue;
    private Vector2 pos;
    private bool isDialogue;

    public NpcData GetNpcData() => npcData;
    public abstract void Interaction();
    
    private void Awake() {
        dialogue = GetComponent<Dialogue>();
    }

    private void Start() {
        pos = Camera.main.WorldToScreenPoint(excuseMeUiPos.position);
    }


    private void Update() {
        CheckPlayer();
        if (isCheck && !isDialogue) {
            DialogueManager.instance.ExcuseMeUI.SetActive(true);
            DialogueManager.instance.ExcuseMeUI.transform.position = pos;
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                ExcuseMe();
            }
        }
        else {
            DialogueManager.instance.ExcuseMeUI.SetActive(false);
        }
    }

    public void ExcuseMe() {
        if (DialogueManager.instance.isEnd == false) return;
        dialogue.StartDialogue();
        DialogueManager.instance.ActiveDialoguePanel(true);
        DialogueManager.instance.Greeting();
    }

    public void SetIsDialogue(bool val) {
        isDialogue = val;
    }

    private void CheckPlayer() {
        isCheck = Physics2D.OverlapBox(transform.position + offset, checkBoxSize, 0, whatIsPlayer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, checkBoxSize);
    }
}
