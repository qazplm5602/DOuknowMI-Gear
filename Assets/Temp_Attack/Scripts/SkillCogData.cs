using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Cog/Skill")]
public class SkillCogData : CogBaseData
{
    // 게임 오브젝트인데 컴포넌트에 공격 모듈 박아야함
    // 모노로 받으면 add컴포넌트 할때 클래스, 인터페이스 타입 관련 문제 생김(빌드 안됨)
    public GameObject loadMoudle;
}