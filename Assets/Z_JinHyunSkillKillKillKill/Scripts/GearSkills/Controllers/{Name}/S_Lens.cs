using FSM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLens : SkillController
{
    public List<Enemy> _enemies = new List<Enemy>();
    Enemy[] _targetEnemies;
    [SerializeField] private LayerMask _weaponLayerMask;
    [SerializeField] private LayerMask _enemyLayerMask;
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
            Vector3 mapSizeHalfExtents = new Vector3(50, 20, 1);




            Collider2D[] cols = Physics2D.OverlapBoxAll(center, mapSizeHalfExtents, 0, _enemyLayerMask);

            //map 있어야 쓸수있음
            //NormalStage currentMap = Map.Instance.CurrentStage as NormalStage;
            //_enemies = currentMap.CurrentEnemies;
            print("overlap 끝");
            foreach (Collider2D col in cols)
            {
                if (col.TryGetComponent(out Enemy enemy))
                {
                    _enemies.Add(enemy);
                }
            }

            print(_enemies.Count);

            if (_enemies.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int idx = Random.Range(0, _enemies.Count);
                    print(idx);
                    print(_enemies[idx].name);
                    print(_enemies[idx].GetComponent<IDamageable>());

                    if (_enemies[idx].TryGetComponent(out IDamageable target))
                    {
                        target.ApplyDamage(Mathf.FloorToInt(_damage), transform);
                    }
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 mapSizeHalfExtents = new Vector3(50, 20, 1);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(PlayerManager.instance.playerTrm.position, mapSizeHalfExtents);
    //}
}
