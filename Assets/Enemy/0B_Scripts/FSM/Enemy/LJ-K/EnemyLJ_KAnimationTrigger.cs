using System.Collections;
using UnityEngine;
using FSM;
using UnityEditor.PackageManager;

public class EnemyLJ_KAnimationTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject _attackRangeObject;

    private SpriteRenderer attackObjSR;
    private EnemyLJ_K _enemyLJ_K;

    private Coroutine _coroutine;

    protected override void Awake() {
        base.Awake();

        _enemyLJ_K = _enemy as EnemyLJ_K;
    }

    private void Start() {
        attackObjSR = _attackRangeObject.GetComponent<SpriteRenderer>();
    }

    private void LookPlayer() {
        _enemy.FlipController(PlayerManager.instance.playerTrm.position.x - _enemy.transform.position.x);
    }

    private void Rush(float velocity) {
        _enemy.SetVelocity(velocity * _enemy.FacingDirection, _enemy.RigidbodyCompo.velocity.y);
    }

    private void Stop() {
        _enemy.StopImmediately(false);
    }

    #region Show Attack Range

    private void ShowAttackRange(float time) {
        _attackRangeObject.transform.localScale = _enemyLJ_K.currentAttackRange;
        _attackRangeObject.transform.localPosition = _enemyLJ_K.currentAttackOffset;
        _coroutine = StartCoroutine(ShowRoutine(time));
    }

    private void HideAttackRange(float time) {
        if(_coroutine != null) StopCoroutine(_coroutine);

        StartCoroutine(HideRoutine(time));
    }

    private IEnumerator ShowRoutine(float time) {
        float timer = 0f;
        while(timer < time) {
            timer += Time.deltaTime;
            attackObjSR.color = new Color(1, 0, 0, timer / time);
            yield return null;
        }
    }

    private IEnumerator HideRoutine(float time) {
        float timer = time;
        while(timer > 0) {
            timer -= Time.deltaTime;
            timer = Mathf.Max(timer, 0f);
            attackObjSR.color = new Color(1, 0, 0, timer / time);
            yield return null;
        }
    }

    #endregion

    private void SpawnStones() {
        CameraManager.Instance.ShakeCamera(10, 10, 0.2f);
        
        int loop = Random.Range(3, 5);
        for(int i = 0; i < loop; ++i) {
            float randXPos = Random.Range(-3f, 2f);
            GameObject obj = Instantiate(_enemyLJ_K.chopStonePrefab, new Vector2
            (_enemyLJ_K.stoneSpawnPosTrm.position.x + randXPos, _enemyLJ_K.stoneSpawnPosTrm.position.y), Quaternion.identity);
            EnemyLJ_KStone stone = obj.GetComponent<EnemyLJ_KStone>();
            stone.Explode(randXPos);
        }
    }

    private void ChangeDoubleAttackSetting() {
        _enemyLJ_K.currentAttackRange = _enemyLJ_K.doubleAttack2Range;
        _enemyLJ_K.currentAttackOffset = _enemyLJ_K.doubleAttack2Offset;
    }

    private void SprayStone(int level) {
        CameraManager.Instance.ShakeCamera(9f, 10f, 0.2f);

        _enemy.AnimatorCompo.speed = 1 + level * 0.5f;
        if(level == 5) _enemy.AnimatorCompo.speed = 1;
        
        int amount = 10 + level * 3;
        float range = 12 + level * 9f;
        for(int i = 0; i < amount; ++i) {
            float randXPos = Random.Range(2f * _enemy.FacingDirection, range * _enemy.FacingDirection);
            float randomSpawnPosX = Random.Range(0.05f * _enemy.FacingDirection, 3f * _enemy.FacingDirection);
            GameObject obj = Instantiate(_enemyLJ_K.sprayStonePrefab, new Vector2
            (_enemyLJ_K.stoneSpawnPosTrm.position.x + randomSpawnPosX, _enemyLJ_K.stoneSpawnPosTrm.position.y), Quaternion.identity);
            EnemyLJ_KStone stone = obj.GetComponent<EnemyLJ_KStone>();
            stone.Explode(randXPos);
        }
    }
}