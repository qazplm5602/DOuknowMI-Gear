using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gear/Gear")]
public class GearSO : ScriptableObject
{
    public string id => base.name.Replace(" (SO),", "");
    [SerializeField] new string name;
    [SerializeField] int cogAmount;
    [SerializeField] GameObject loadModule; // 스킬 관련된 스크를 오브젝트에 감싸서 넣어야함.

    // @Getter
    GameObject LoadModule => loadModule;

    int CogAmount => cogAmount;

    string Name => name;
}