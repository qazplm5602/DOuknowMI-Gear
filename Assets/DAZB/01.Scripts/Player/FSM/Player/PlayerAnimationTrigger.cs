using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Player player;
    private void AnimationEnd() {
        player.StateMachine.CurrentState.AnimationFinishTrigger();
    }
}
