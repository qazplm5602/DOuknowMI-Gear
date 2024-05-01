using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Gear/Enforce")]
public class GearEnforceSO : ScriptableObject
{
    [SerializeField] int[] damages;
    [SerializeField] float[] ranges;

    public int[] Damages => damages;
    public float[] Ranges => ranges;
}
