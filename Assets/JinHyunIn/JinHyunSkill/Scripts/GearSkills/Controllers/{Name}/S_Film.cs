using FSM;
using UnityEngine;

public class SkillFilm  : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable target))
        {
            EntityStat modifyingStat = collision.GetComponent<Enemy>().Stat;

            if (modifyingStat == null) return;
            foreach (StatType t in _willModify)
            {
                modifyingStat.AddModifierByTime(_debuffValue, t, _debuffTime);
            }
            
            target.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
            if (_canPierce)
            {
                --_pierceCount;
            }
        }
    }

}