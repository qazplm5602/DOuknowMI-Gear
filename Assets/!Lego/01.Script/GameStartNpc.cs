using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class GameStartNpc : MonoBehaviour, IInteraction
{
    [SerializeField] private string _stageSceneName;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 checkBoxSize;
    [SerializeField] private Vector3 offset;

    private bool _isCheck;

    public void Interaction()
    {
        SceneManager.LoadScene(_stageSceneName);
    }

    private void Update()
    {
        CheckPlayer();
        if (_isCheck)
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                Interaction();
            }
        }
        else
        {
            if (DialogueManager.instance.npc == null) return;
            if (DialogueManager.instance.npc.name == gameObject.name)
            {
                DialogueManager.instance.ExcuseMeUI.SetActive(false);
            }
        }

    }
    private void CheckPlayer() => _isCheck = Physics2D.OverlapBox(transform.position + offset, checkBoxSize, 0, whatIsPlayer);
}
