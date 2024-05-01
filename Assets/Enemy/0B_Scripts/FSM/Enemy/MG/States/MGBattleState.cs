using UnityEngine;
using FSM;

public class MGBattleState : EnemyState<MGStateEnum>
{
    public MGBattleState(Enemy enemy, EnemyStateMachine<MGStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyMG _enemyMG;

    private Transform _playerTrm;
    private float _jumpTimer = 0f;

    public override void Enter() {
        base.Enter();

        _enemyMG = _enemy as EnemyMG;

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.AnimatorCompo.SetFloat(_enemyMG.battleModeHash, 0);
    }

    public override void UpdateState() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        float distance = Vector2.Distance(_enemy.transform.position, _playerTrm.position);

        if(Mathf.Abs(direction.x) >= 0.1f)
            _enemy.FlipController(direction.x);
        else {
            _enemy.StopImmediately(false);
        }

        if(_enemy.CanAttack() && !_enemy.IsObstacleInLine(distance, direction.normalized) && distance < _enemyMG.combatAttackRange) {
            _stateMachine.ChangeState(MGStateEnum.Punch);
        }
        else if(_enemy.CanAttack() && !_enemy.IsObstacleInLine(distance, direction.normalized) && distance < _enemyMG.rangeAttackRange) {
            _stateMachine.ChangeState(MGStateEnum.Spit);
        }
        else if(distance <= _enemy.nearDistance) {
            _enemy.StopImmediately(false);
            _enemy.AnimatorCompo.SetFloat(_enemyMG.battleModeHash, 0);
        }
        else {
            _enemy.AnimatorCompo.SetFloat(_enemyMG.battleModeHash, 1);
            _enemy.SetVelocity(_enemy.FacingDirection * _enemy.Stat.moveSpeed.GetValue(), _rigidbody.velocity.y);
        }
        
        if(_enemy.IsOnPlatform()) {
            if(direction.y <= -1f && Mathf.Abs(direction.x) < 3f) {
                _enemy.downJumpTimer += Time.deltaTime;

                if(_enemy.downJumpTimer >= 2f) {
                    _enemy.DownJump();
                    _enemy.downJumpTimer = 0f;
                }
            }
            else {
                _enemy.downJumpTimer -= Time.deltaTime / 2f;
            }
        }
        else _enemy.downJumpTimer -= Time.deltaTime / 2f;

        if(_enemy.downJumpTimer < 0f) _enemy.downJumpTimer = 0f;

        if(_enemy.IsUnderPlatform()) {
            Debug.Log(1);
            if(direction.y >= 1f && Mathf.Abs(direction.x) < 3f) {
                _jumpTimer += Time.deltaTime;
                Debug.Log(_jumpTimer);

                if(_jumpTimer >= 2f) {
                    float jump = Physics2D.Raycast(_enemy.transform.position, Vector2.up, 4f, _enemy.whatIsPlatform).distance;
                    _enemy.SetVelocity(_enemy.RigidbodyCompo.velocity.x, jump + _enemy.jumpPower);
                    _jumpTimer = 0f;
                }
            }
            else {
                _jumpTimer -= Time.deltaTime / 2f;
            }
        }
        else _jumpTimer -= Time.deltaTime / 2f;

        if(_jumpTimer < 0f) _jumpTimer = 0f;
    }
}
