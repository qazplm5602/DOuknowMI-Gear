using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SkillLens : SkillController
{
    [SerializeField] private LayerMask _weaponLayerMask;
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _weaponLayerMask)
        {
            Vector3 center = PlayerManager.instance.playerTrm.position;
            Vector3 mapSizeHalfExtents = new Vector3(30, 15, 1);
            //Collider[] enemies = Map.Instance.CurrentStage.; // 맵매니저에 현재맵의 뭐 가져오는거로할건데 일단 지금은 개똥쓰레기상태
            
            //foreach (var enemy in enemies)
            //{
            //    if (enemy.TryGetComponent(out IDamageable target))
            //    {
            //        target.ApplyDamage(Mathf.FloorToInt(_damage), null);
            //    }
            //}
        }
    }
}
