using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class SkillController : MonoBehaviour
{
    #region 스킬 정보
    [SerializeField] 
    private int _damage;
    [SerializeField] 
    private CastingType castType = CastingType.None;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField] 
    private float _maxRange;
    [SerializeField]
    protected float _debuffTime;
    #endregion

    #region 캐스팅 관련 정보
    [SerializeField] 
    private Vector2 _castPos;
    [SerializeField]
    private Vector2 _castSize;
    [SerializeField]
    private float _castAngle;
    [SerializeField]
    private float _castRadius;

    [SerializeField] 
    private bool isDamageCasting;
    private bool _attackTriggerCalled;
    #endregion

    protected virtual IEnumerator MoveRoutine(Transform startTrm)
    {
        Vector3 firstPos = startTrm.position;
        bool notMaxDistance = IsInRange(firstPos, currentPos : transform.position);
        while (notMaxDistance)  
        {
            notMaxDistance = IsInRange(firstPos, currentPos : transform.position);

            transform.position += _moveSpeed * Time.deltaTime * startTrm.right;
            if (isDamageCasting && _attackTriggerCalled) DamageCasting();

            yield return null;
        }
        if (isDamageCasting) DamageCasting();
        Destroy(gameObject);
        yield break;
    }

    bool IsInRange(Vector3 firstPos, Vector3 currentPos)
    {
        return Vector3.Distance(firstPos, currentPos) < _maxRange;
    }

    public void BNNAttackTrigger()
    {
        return;
        //_attackTriggerCalled = true;
    }

    private void DamageCasting()
    {
        _attackTriggerCalled = false;
        PlayerSkillManager.Instance.gearSkillDamageCaster.DamageCast(_damage, _castPos, _castSize, _castAngle, _castRadius, castType);
        return;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable target))
        {
            Debug.Log($"{collision.gameObject.name}(이)가 맞음");
            //PlayerManager.instance.transform 넣으면 되는거임?
            target.ApplyDamage(_damage, null);
        }
    }

    protected virtual void ModifyEnemyStat(float value, StatType statType, float time) 
    {
        foreach (Entity item in StageManager.Instance._enemies)
        {
            EntityStat modifyingStat = item.Stat;

            if (modifyingStat == null) return;

            modifyingStat.AddModifierByTime(value, statType, time);
            print($"Add : {statType}");
        }
    }
}
