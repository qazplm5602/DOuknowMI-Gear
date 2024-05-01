using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gear/DB")]
public class GearDatabase : ScriptableObject
{
    [SerializeField] GearSO[] list;
    Dictionary<string, GearSO> indexing;
    
    void StartIndex() {
        indexing = new();
        foreach (var item in list)
            indexing[item.id] = item;
    }

    public GearSO GetGearById(string id) {
        if (indexing == null)
            StartIndex();

        return indexing[id];
    }
}
