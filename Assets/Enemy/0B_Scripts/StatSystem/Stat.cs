using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _baseValue;
    public List<float> modifiers;
    public bool isPercent;
    private float defaultValue;

    public float GetValue() {
        float finalValue = _baseValue;
        foreach(float value in modifiers) {
            finalValue += value;
        }
        return finalValue;
    }

    public void Init() {
        defaultValue = _baseValue;
    }

    public void AddModifier(float value) {
        if(value != 0) modifiers.Add(value);
    }

    public void RemoveModifier(float value) {
        if(value != 0) modifiers.Remove(value);
    }

    public void SetDefaultValue(float value) {
        _baseValue = value;
    }

    public void ResetStat() {
        _baseValue = defaultValue;
    }

    public float GetDefaultValue() {
        return defaultValue;
    }
}
