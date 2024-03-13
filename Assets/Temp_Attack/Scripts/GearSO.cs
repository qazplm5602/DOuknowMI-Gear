using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CogType {
    None,
    Skill,
    Link
}

[CreateAssetMenu(menuName = "SO/Gear/Gear")]
public class GearSO : ScriptableObject
{
    public string id => base.name.Replace(" (SO),", "");
    [SerializeField] new string name;
    [SerializeField] int cogAmount;
    [SerializeField] int linkAmount;
    [SerializeField] GameObject loadModule; // 스킬 관련된 스크를 오브젝트에 감싸서 넣어야함.

    // @Getter
    public GameObject LoadModule => loadModule;

    public int CogAmount => cogAmount;

    public string Name => name;
}