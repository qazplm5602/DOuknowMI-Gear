using FSM;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class SkillController : MonoBehaviour
{
    #region 스킬 정보
    [SerializeField] 
    protected float _damage;
    [SerializeField] 
    protected CastingType castType = CastingType.None;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField] 
    protected float _maxRange;
    [SerializeField]
    protected bool _destroyByTime = false;
    [SerializeField]
    protected float _destroyTime = 0.0f;
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
    #endregion

    private bool _attackTriggerCalled;

    #region 스탯을 건드리는 아이들
    [SerializeField]
    protected List<StatType> _willModify = new();
    [SerializeField]
    protected float _debuffTime;
    [Tooltip("0.5를 넣으면 현재 스탯의 50%만큼 감소시킴. *-1 된 값으로 적용")][SerializeField]
    protected float _debuffValue = 0f;
    #endregion

    #region 스탯 건드는 코드
    //foreach (StatType t in _willModify)
    //{
    //        ModifyEnemyStat(_debuffValue, t, _debuffTime);
    //}
    #endregion

protected virtual IEnumerator MoveRoutine(Transform startTrm)
    {
        _damage += PlayerManager.instance.player.stat.attack.GetValue();
        if (_destroyByTime)
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
            yield break;
        }
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

    protected bool IsInRange(Vector3 firstPos, Vector3 currentPos)
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
        PlayerSkillManager.Instance.gearSkillDamageCaster.DamageCast(Mathf.FloorToInt(_damage), _castPos, _castSize, _castAngle, _castRadius, castType);
        return;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable target))
        {
            Debug.Log($"{collision.gameObject.name}(이)가 맞음");
            //PlayerManager.instance.transform 넣으면 되는거임?
            target.ApplyDamage(Mathf.FloorToInt(_damage), null);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out IDamageable target))
        {
            Debug.Log($"{collision.gameObject.name}(이)가 맞음");
            //PlayerManager.instance.transform 넣으면 되는거임?
            target.ApplyDamage(Mathf.FloorToInt(_damage), null);
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
