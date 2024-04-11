using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "SO/Stat")]
public class EntityStat : ScriptableObject
{
    public Stat maxHealth;
    public Stat attack;
    public Stat moveSpeed;
    public Stat attackSpeed;
    public Stat attackRange;

    protected Entity _onwer;
    protected Dictionary<StatType, Stat> _statDictionary;

    public void SetOwner(Entity entity) {
        _onwer = entity;
    }

    protected virtual void OnEnable() {
        _statDictionary = new Dictionary<StatType, Stat>();

        Type entityStatType = typeof(EntityStat);

        foreach(StatType statEnum in Enum.GetValues(typeof(StatType))) {
            try {
                string fieldName = LowerFirstChar(statEnum.ToString());
                FieldInfo statField = entityStatType.GetField(fieldName);

                Stat statInstance = statField.GetValue(this) as Stat;

                _statDictionary.Add(statEnum, statInstance);
            }
            catch(Exception e) {
                Debug.LogError($"There is no stat : {statEnum} [{e.Message}]");
            }
        }
    }

    private string LowerFirstChar(string input) => char.ToLower(input[0]) + input[1..];

    public float GetAttackSpeed() {
        return attackSpeed.GetValue() * 0.001f;
    }
}
