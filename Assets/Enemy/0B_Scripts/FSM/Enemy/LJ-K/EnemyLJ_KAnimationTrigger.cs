using UnityEngine;
using FSM;
using System.Collections;

public class EnemyLJ_KAnimationTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject _attackRangeObject;

    private EnemyLJ_K _enemyLJ_K;

    protected override void Awake() {
        base.Awake();

        _enemyLJ_K = _enemy as EnemyLJ_K;
    }

    private void ViewAttackRange() {
        _attackRangeObject.SetActive(true);
        _attackRangeObject.transform.localScale = _enemyLJ_K.currentAttackRange;
        _attackRangeObject.transform.localPosition = _enemyLJ_K.currentAttackOffset;
        StartCoroutine(ViewRoutine());
    }

    private IEnumerator ViewRoutine() {
        float timer = 0f;
        SpriteRenderer attackObjSR = _attackRangeObject.GetComponent<SpriteRenderer>();
        while(timer < 1.5f) {
            timer += Time.deltaTime;
            attackObjSR.color = new Color(1, 0, 0, timer / 1.5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        timer = 0f;
        while(timer < 0.15f) {
            timer += Time.deltaTime;
            attackObjSR.color = new Color(1, 0, 0, (0.15f - timer) * 6.67f);
            yield return null;
        }
        _attackRangeObject.SetActive(false);
    }
}
