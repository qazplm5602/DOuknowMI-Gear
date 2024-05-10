using FSM;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLens : SkillController
{
    public List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] private LayerMask _weaponLayerMask;
    [SerializeField] private LayerMask _enemyLayerMask;
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        int layerMaskValue = (int)Mathf.Log(_weaponLayerMask.value, 2);
        if (collision.gameObject.layer == (int)Mathf.Log(_enemyLayerMask, 2)) return;
        if (collision.gameObject.layer == layerMaskValue)
        {
            SoundManager.Instance.PlaySound(SoundType.LensBling);
            if (Map.Instance == null) return;
            _enemies = (Map.Instance.CurrentStage as NormalStage).CurrentEnemies;

            if (_enemies.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int idx = Random.Range(0, _enemies.Count);
                    if (_enemies.Count != 0 && _enemies[idx].TryGetComponent(out IDamageable target))
                    {
                        target.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
                    }
                }
            }
        }
    }
}
