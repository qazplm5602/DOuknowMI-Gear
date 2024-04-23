using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AgentStat")]
public class AgentStat : ScriptableObject
{
    public Stat maxHealth;
    public Stat attack;
    public Stat moveSpeed;
    public Stat attackSpeed;
    public Stat defense;
    public Stat criticalChance;

    protected Agent _onwer;
    protected Dictionary<PlayerStatType, Stat> _statDictionary;

    public void SetOwner(Agent agent) {
        _onwer = agent;
    }

    protected virtual void OnEnable() {
        _statDictionary = new Dictionary<PlayerStatType, Stat>();

        Type playerStatType = typeof(AgentStat);

        foreach(PlayerStatType statEnum in Enum.GetValues(typeof(PlayerStatType))) {
            try {
                string fieldName = LowerFirstChar(statEnum.ToString());
                FieldInfo statField = playerStatType.GetField(fieldName);

                Stat statInstance = statField.GetValue(this) as Stat;;

                _statDictionary.Add(statEnum, statInstance);
            }
            catch(Exception e) {
                Debug.LogError($"There is no stat : {statEnum} [{e.Message}]");
            }
        }
    }

    private string LowerFirstChar(string input) => char.ToLower(input[0]) + input[1..];

    public float GetCriticalChance() {
        return criticalChance.GetValue() * 0.001f;
    }

    public float GetMoveSpeed() {
        return moveSpeed.GetValue() * 0.001f;
    }

    public float GetAttackSpeed() {
        return attackSpeed.GetValue() * 0.001f;
    }
}
