using UnityEngine;

public class GearBNNSkill : GearCogEvent
{
    private PlayerSkill _skillType = PlayerSkill.BNN;
    //[SerializeField] GameObject anyObj;

    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion look = AngleManager.GetTargetDirection(playerPos, mousePos);

        GameObject prefab = PlayerSkillManager.Instance.playerSkill[_skillType];

        Instantiate(prefab, playerPos, look);

    }
}
        //PlayerSkillManager.Instance.skillRoutine[_skillType]
        //    .Enqueue(StartCoroutine(MoveSkillGameObejct(_maxRange, startTrm)));

    //private IEnumerator MoveSkillGameObejct(float range, Transform startTrm)
    //{
    //    bool notMaxDistance = Vector3.Distance(startTrm.position, _skillPrefab.transform.position) < range;
    //    while(notMaxDistance == true || _skillPrefab == null)
    //    {
    //        _skillPrefab.transform.position += _moveSpeed * Time.deltaTime * startTrm.forward;
    //        //이동 말고도 뭔가 할 게 있으면 좋겠는데 아직은떠오른느게없다
    //        //가 아니잖아 ㅅㅂ
    //        //animation trigger called 일때 DamageCast

    //        //CastInRoutine
    //        yield return null;
    //    }
    //    //만약 포탄 폭발같은 거라면 여기서.
    //    //DamageCasting
    //    yield break;
    //}

    //Damagecasting 
    //private void CastInRoutine(bool isDamageCasting)
    //{
    //    if (isDamageCasting == true && _attackTriggerCalled)
    //    {
    //        //화포의 경우 이동하면서 발사해야하니까 여기서.
    //        //근데 얜 없음
    //        return;
    //        //DamageCasting();  
    //        //_attackTriggerCalled = false;
    //    }
    //}

    //public void BNNAttackTrigger()
    //{
    //    return;
    //    //_attackTriggerCalled = true;
    //}

    //private void DamageCasting()
    //{
    //    return;
    //    //PlayerSkillManager.Instance.gearSkillDamageCaster.DamageCast(_damage, castPos, 0, 0, 0, CastingType.None);
    //}

