using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "SO/EntityStat")]
public class EntityStat : ScriptableObject
{
    public Stat maxHealth;
    public Stat attack;
    public Stat moveSpeed;
    public Stat attackSpeed;
    public Stat attackCooldown;
    public Stat attackRange;

    protected Entity _owner;
    protected Dictionary<StatType, Stat> _statDictionary;

    public void SetOwner(Entity entity) {
        _owner = entity;
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

    public void AddModifierByTime(float value, StatType statType, float time)
    {
        _owner.StartCoroutine(StatRoutine(value, statType, time));
    }

    private IEnumerator StatRoutine(float value, StatType statType, float time)
    {
        Type entityStatType = typeof(EntityStat);
        string fieldName = LowerFirstChar(statType.ToString());
        FieldInfo statField = entityStatType.GetField(fieldName);

        Stat statInstance = statField.GetValue(this) as Stat;
        float amount = statInstance.GetValue() * value * -1;

        Debug.Log($"[{statInstance}]{statField} modify value : {amount}");
        statInstance.AddModifier(amount);
        yield return new WaitForSeconds(time);
        statInstance.RemoveModifier(amount);
    }
}
