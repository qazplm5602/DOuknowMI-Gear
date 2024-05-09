using FSM;
using UnityEngine;

public class SkillFilm  : SkillController
{
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundType.FilmRolling);
        StartCoroutine(MoveRoutine(transform));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == _wallLayerInfo)
            || (collision.gameObject.layer == _groundLayerInfo))
        {
            PoolManager.Instance.Pop(PoolingType.Effect_Wall, true, transform);
            Destroy(gameObject);
        }
        if (collision.TryGetComponent(out IDamageable target))
        {
            EntityStat modifyingStat = collision.GetComponent<Enemy>().Stat;

            if (modifyingStat == null) return;
            foreach (StatType t in _willModify)
            {
                modifyingStat.AddModifierByTime(_debuffValue, t, _debuffTime);
            }
            
            PoolManager.Instance.Pop(PoolingType.Effect_Impact, true, collision.transform);
            target.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
            if (_canPierce)
            {
                --_pierceCount;
            }
        }
    }

}