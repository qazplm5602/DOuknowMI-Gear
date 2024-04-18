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

    protected virtual void ModifyEnemyStat(StatType statFieldName, float percent, bool isAdd) 
    {
        //string statFieldString = statFieldName.ToString();
        //string firstLowerStatFieldName = $"{char.ToLower(statFieldString[0])}{statFieldString[1..]};

        Type t = typeof(EntityStat);
        FieldInfo fieldInfo = t.GetField(statFieldName.ToString(), BindingFlags.IgnoreCase);

        //FieldInfo fieldInfo = t.GetField(firstLowerStatFieldName);


        List<Enemy> enemies = new List<Enemy>();
        //enemies.Add(FindObjectOfType<Enemy>());
        //현재맵.적들

        foreach (Enemy item in enemies)
        {
            Stat modifyingStat = fieldInfo.GetValue(item.Stat) as Stat;

            if (modifyingStat == null) return;

            if (isAdd)
            {
                float value = modifyingStat.GetValue() * percent;
                modifyingStat.AddModifier(value);
            }
            else
            {
                float value = modifyingStat.GetValue() / percent;
                modifyingStat.RemoveModifier(value);
            }
        }
    }
}
