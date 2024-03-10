using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBaseData : ScriptableObject
{
    public enum CogDBType {
        None,
        Skill,
        Link,
        Combine
    }

    public string id => base.name.Replace(" (SO)", "");
    public new string name;
    public Sprite image;

    // 타입 확인
    public CogDBType GetCogType() {
        if (this is SkillCogData) return CogDBType.Skill;
        if (this is LinkCogData) return CogDBType.Link;
        if (this is CombineLinkCogData) return CogDBType.Combine;

        return CogDBType.None;
    }
    
    // 이거 좀 편함 ex: SkillCogData == CogDBType.Skill
    // public static bool operator ==(CogBaseData cog, CogDBType cogType) {
    //     return cog.GetCogType() == cogType;
    // }

    // public static bool operator !=(CogBaseData cog, CogDBType cogType) {
    //     return cog.GetCogType() != cogType;
    // }
}