using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Npc : MonoBehaviour, IInteraction
{
    [SerializeField] private NpcData npcData;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 checkBoxSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform excuseMeUiPos;
    [SerializeField] private Transform nameTagPos;
    [SerializeField] private string interactionName;

    protected bool isCheck;
    private Dialogue dialogue;
    private Vector2 ecUiPos;
    private Vector2 nTPos;
    private bool isDialogue;
    private TMP_Text excuseMeText;
    private TMP_Text nameTagText;
    private ExeuseMeUI exeuseMeUI;

    public NpcData GetNpcData() => npcData;
    public abstract void Interaction();
    private void Awake() {
        dialogue = GetComponent<Dialogue>();
        excuseMeText = DialogueManager.Instance.ExcuseMeUI.GetComponentInChildren<TMP_Text>(false); 
        nameTagText = DialogueManager.Instance.NameTag.GetComponentInChildren<TMP_Text>(false);
        exeuseMeUI = DialogueManager.Instance.ExcuseMeUI.GetComponent<ExeuseMeUI>();
    }

    private void Update() {
        CheckPlayer();
        if (isCheck && !isDialogue) {
            if (excuseMeText != null) {
                excuseMeText.text = interactionName;
            }
            ecUiPos = Camera.main.WorldToScreenPoint(excuseMeUiPos.position);
            nTPos = Camera.main.WorldToScreenPoint(nameTagPos.position);
            DialogueManager.Instance.ExcuseMeUI.SetActive(true);
            DialogueManager.Instance.ExcuseMeUI.transform.position = ecUiPos;
            DialogueManager.Instance.SetNpc(this);
            nameTagText.text= npcData.Name + "\n" + npcData.Job;
            DialogueManager.Instance.NameTag.SetActive(true);
            DialogueManager.Instance.NameTag.transform.position = nTPos;
            exeuseMeUI.SetPosition();
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                ExcuseMe();
            }
        }
        else {
            if (DialogueManager.Instance.npc == null || DialogueManager.Instance.checkInteractiveObejct) return;
            if (DialogueManager.Instance.npc.name == gameObject.name) {
                DialogueManager.Instance.ExcuseMeUI.SetActive(false);
                DialogueManager.Instance.NameTag.SetActive(false);
            }
        }
    }

    public void ExcuseMe() {
        if (DialogueManager.Instance.isEnd == false) return;
        PlayerManager.instance.player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        dialogue.StartDialogue();
        DialogueManager.Instance.ActiveDialoguePanel(true);
        DialogueManager.Instance.Greeting();
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
