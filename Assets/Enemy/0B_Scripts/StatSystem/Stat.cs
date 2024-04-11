using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;
    public List<int> modifiers;
    public bool isPercent;

    public int GetValue() {
        int finalValue = _baseValue;
        foreach(int value in modifiers) {
            finalValue += value;
        }

        return finalValue;
    }

    public void AddModifier(int value) {
        if(value != 0) modifiers.Add(value);
    }

    public void RemoveModifier(int value) {
        if(value != 0) modifiers.Remove(value);
    }

    public void SetDefaultValue(int value) {
        _baseValue = value;
    }
}
