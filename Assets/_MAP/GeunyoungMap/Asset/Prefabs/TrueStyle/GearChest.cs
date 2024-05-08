using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChest : InteractiveObject
{
    private Animator _animator;
    public GearSO[] so = new GearSO[3];
    public GearDatabase GEAR_DB;

    private readonly int _openAnimHash = Animator.StringToHash("Open");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        so[0] = GEAR_DB.GetRandomGear();
        while (so[1] != null && so[0] == so[1])
        {
            so[1] = GEAR_DB.GetRandomGear();
        }
        while (so[2] != null && (so[0] == so[2] || so[1] == so[2]))
        {
            so[2] = GEAR_DB.GetRandomGear();
        }
    }

    public override void Interaction()
    {
        _animator.SetTrigger(_openAnimHash);

        IngameUIControl.Instance.CHESTUI.Show(so);
        Destroy(gameObject, 2f);
    }
}
