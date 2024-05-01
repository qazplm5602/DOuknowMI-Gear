using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gear/Link")]
public class GearLinkSO : ScriptableObject
{
    [SerializeField] GearSO[] combine;
    [SerializeField] GameObject loadModule;

    public GameObject LoadModule {
        get => loadModule;
    }

    public GearSO[] Combine {
        get => combine;
    }

    public string GetId() {
        List<string> cogIds = new();

        foreach (var item in combine)
            cogIds.Add(item.id);

        return System.String.Join(",", cogIds.OrderBy(v => v).ToArray());
    }
}
