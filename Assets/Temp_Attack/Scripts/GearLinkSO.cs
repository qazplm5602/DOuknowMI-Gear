using System.Collections;
using System.Collections.Generic;
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
}
