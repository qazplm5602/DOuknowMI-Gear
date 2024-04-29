using FSM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class SkillLens : SkillController
{
    List<Enemy> _enemies = new List<Enemy>();
    Enemy[] _targetEnemies;
    [SerializeField] private LayerMask _weaponLayerMask;
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        print("시작1");
        if (collision.CompareTag("PlayerWeapon"))
        {
            print("시작");
            Vector3 center = PlayerManager.instance.playerTrm.position;
            Vector3 mapSizeHalfExtents = new Vector3(30, 15, 1);



            //NormalStage currentMap = Map.Instance.CurrentStage as NormalStage;
            
            //foreach (var enemy in currentMap.CurrentEnemies)
            //{
            //    if (enemy.TryGetComponent(out IDamageable target))
            //    {
            //        _enemies.Add(enemy);
            //    }
            //}
            //print(_enemies.Count);
            //_targetEnemies = _enemies.OrderByDescending(x => Vector2.Distance(transform.position, x.transform.position)).ToArray();
            foreach (Enemy item in _targetEnemies)
            {
                print(item.name);
            }

            if (_targetEnemies.Length > 0)
            {
                for(int i = 0; Mathf.Clamp(i, 0, 3) < _targetEnemies.Length; i++)
                {
                    _targetEnemies[i].GetComponent<IDamageable>().ApplyDamage(Mathf.FloorToInt(_damage), transform);
                    print($"damage to {_targetEnemies[i].name}");
                }
            }
        }
    }
}
