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
    public Sprite icon;
    [SerializeField] new string name;
    [SerializeField, TextArea] string desc;

    [Header("Default Values")]
    [SerializeField] int defaultDamage;
    [SerializeField] float defaultRange;

    [Header("Options")]
    [SerializeField] bool activeDamage = true;
    [SerializeField] bool activeRange = true;

    [Space]
    [SerializeField] CogType[] cogList;
    [SerializeField] GearEnforceSO enforceData; // 기어 강화 수치
    [SerializeField] GameObject loadModule; // 스킬 관련된 스크를 오브젝트에 감싸서 넣어야함.

    // @Getter
    public GameObject LoadModule => loadModule;
    public CogType[] CogList => cogList;
    public GearEnforceSO EnforceData => enforceData;

    // Getter
    public string Name => name;
    public Sprite Icon => icon;
    public string Desc => desc;

    public int DefaultDamage => defaultDamage;
    public float DefaultRange => defaultRange;

    public bool ActiveDamage => activeDamage;
    public bool ActiveRange => activeRange;
}