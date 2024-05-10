using UnityEngine;
using UnityEngine.InputSystem;

public class GameStartNpc : MonoBehaviour, IInteraction
{
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 checkBoxSize;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform excuseMeUIPos;
    private Vector2 _pos;

    private bool _isCheck;

    public void Interaction()
    {
        VillageManager.Instance.ChangeSceneToStage();
    }

    private void Update()
    {   
        CheckPlayer();
        if (_isCheck)
        {
            //여기
            _pos = Camera.main.WorldToScreenPoint(excuseMeUIPos.position);
            DialogueManager.Instance.ExcuseMeUI.SetActive(true);
            DialogueManager.Instance.ExcuseMeUI.transform.position = _pos;

            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                Interaction();
            }
        }
        else
        {
            DialogueManager.Instance.ExcuseMeUI.SetActive(false);
        }
    }
    private void CheckPlayer() => _isCheck = Physics2D.OverlapBox(transform.position + offset, checkBoxSize, 0, whatIsPlayer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, checkBoxSize);
    }
}