using FSM;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    protected int _pierceCount;
    [SerializeField]
    protected bool _canPierce = false;
    [SerializeField]
    protected LayerMask _groundLayerMask;
    [SerializeField]
    protected LayerMask _wallLayerMask;

    protected int _wallLayerInfo;
    protected int _groundLayerInfo;
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
    [Header("Modify Stat")]
    [SerializeField]
    protected List<StatType> _willModify = new();
    [SerializeField]
    protected float _debuffTime;
    [Tooltip("0.5를 넣으면 현재 스탯의 50%만큼 감소시킴. *-1 된 값으로 적용")]
    [SerializeField]
    protected float _debuffValue = 0f;
    #endregion
    private void Awake()
    {
        _wallLayerInfo = (int)Mathf.Log(_wallLayerMask.value, 2);
        _groundLayerInfo = (int)Mathf.Log(_groundLayerMask.value, 2);
        _damage += PlayerManager.instance.player.stat.attack.GetValue();
    }
    protected virtual IEnumerator MoveRoutine(Transform startTrm)
    {
        //_damage += PlayerManager.instance.player.stat.attack.GetValue();
        if (_destroyByTime)
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
            yield break;
        }
        Vector3 firstPos = startTrm.position;
        bool notMaxDistance = IsInRange(firstPos, currentPos: transform.position);
        while (notMaxDistance)
        {
            notMaxDistance = IsInRange(firstPos, currentPos: transform.position);

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

    protected virtual void DamageCasting()
    {
        _attackTriggerCalled = false;
        PlayerSkillManager.Instance.gearSkillDamageCaster.DamageCast(Mathf.FloorToInt(_damage), _castPos, _castSize, _castAngle, _castRadius, castType);
        return;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == _wallLayerInfo)
            || (collision.gameObject.layer == _groundLayerInfo))
        {
            PoolManager.Instance.Pop(PoolingType.Effect_Wall, true, transform);
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out IDamageable target))
        {
            PoolManager.Instance.Pop(PoolingType.Effect_Impact, true, collision.transform);
            target.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
            if (_canPierce)
            {
                if (_pierceCount <= 0) Destroy(gameObject);
                --_pierceCount;
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == _wallLayerInfo)
            || (collision.gameObject.layer == _groundLayerInfo))
        {
            PoolManager.Instance.Pop(PoolingType.Effect_Wall, true, transform);
            Destroy(gameObject);
        }
        if (collision.collider.TryGetComponent(out IDamageable target))
        {
            PoolManager.Instance.Pop(PoolingType.Effect_Impact, true, collision.transform);
            target.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
            if (_canPierce)
            {
                --_pierceCount;
            }
        }
    }

    protected virtual void ModifyEnemyStat(float value, StatType statType, float time)
    {
        foreach (Entity item in (Map.Instance.CurrentStage as NormalStage).CurrentEnemies)
        {
            EntityStat modifyingStat = item.Stat;

            if (modifyingStat == null) return;

            modifyingStat.AddModifierByTime(value, statType, time);
        }
    }
}
