using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkill
{
    BNN,
    SH,
    MH,
    HH,
    Clock,
    Wheel,
    Coal,
    Foghorn,
    SteamLoco,
    Shell,
    Artillery,
    Engine,
    Car,
    Film,
    Lens,
    Camera
}

public class PlayerSkillManager : MonoSingleton<PlayerSkillManager>
{
    [HideInInspector]
    public GearSkillDamageCaster gearSkillDamageCaster;

    public Dictionary<PlayerSkill, GameObject> playerSkill = new();
    public GameObject[] skillPrefabs;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            playerSkill[(PlayerSkill)i] = skillPrefabs[i];
        }
        gearSkillDamageCaster = GetComponent<GearSkillDamageCaster>();
    }
}
