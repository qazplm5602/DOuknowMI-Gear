using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BNN : SkillController
{
    //여기서 뭘 더 해줄진 모르겠음
    //해준다면 DamageCast가 필요한 애의 경우에 Override해서 DamageCast조건을 조금 건드는 정도

    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
