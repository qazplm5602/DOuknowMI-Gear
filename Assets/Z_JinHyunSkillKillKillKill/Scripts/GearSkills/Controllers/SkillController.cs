using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    internal virtual IEnumerator MoveRoutine(Transform startTrm)
    {
        bool notMaxDistance = Vector3.Distance(startTrm.position, transform.position) < _maxRange;
        while (Vector3.Distance(startTrm.position, transform.position) < _maxRange == true)
        {
            transform.position += _moveSpeed * Time.deltaTime * startTrm.right;

            //animation trigger called 일때 DamageCast
            if (isDamageCasting && _attackTriggerCalled)
                DamageCasting();

            //뭔가 종료조건 추가하ㅣ고싶은데 애매하다

            yield return null;
        }
        if (isDamageCasting) DamageCasting();
        //Destroy(gameObject);
        yield break;
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
}
