using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class DemonSlimeDeadState : EnemyState<DemonSlimeStateEnum>
{
    public DemonSlimeDeadState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }
}
