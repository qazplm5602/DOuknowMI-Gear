using FSM;
using System.Collections;
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
            Debug.Log($"{collision.gameObject.name}(이)가 맞음");
            //PlayerManager.instance.transform 넣으면 되는거임?
            EntityStat modifyingStat = collision.GetComponent<Enemy>().Stat;

            if (modifyingStat == null) return;
            foreach (StatType t in _willModify)
            {
                modifyingStat.AddModifierByTime(_debuffValue, t, _debuffTime);
            }
            --_pierceCount;

            target.ApplyDamage(Mathf.FloorToInt(_damage), null);
        }
    }
}
