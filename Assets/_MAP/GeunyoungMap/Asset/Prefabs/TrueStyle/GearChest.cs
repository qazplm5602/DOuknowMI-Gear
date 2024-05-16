using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GearChest : InteractiveObject
{
    private Animator _animator;
    public GearSO[] so = new GearSO[3];
    public GearDatabase GEAR_DB;
    public Image screen; 
    private bool isOpened;

    private readonly int _openAnimHash = Animator.StringToHash("Open");

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        so[0] = GEAR_DB.GetRandomGear();
        while (so[1] == null || so[0] == so[1])
        {
            so[1] = GEAR_DB.GetRandomGear();
        }
        while (so[2] == null || (so[0] == so[2] || so[1] == so[2]))
        {
            so[2] = GEAR_DB.GetRandomGear();
        }
    }

    protected override void Update() {
        if (isOpened) return;
        base.Update();
    }

    public override void Interaction()

    {
        isOpened = true;
        DialogueManager.Instance.ExcuseMeUI.SetActive(false);
        _animator.SetTrigger(_openAnimHash);

        IngameUIControl.Instance.CHESTUI.Show(so);
        StartCoroutine(GOHOme());
        
    }

    private IEnumerator GOHOme()
    {
        screen.DOFade(1, 1f);
        screen.transform.parent = FindObjectOfType<GearManager>().GetComponent<Canvas>().transform;
        screen.rectTransform.anchoredPosition = new Vector2(0, 0);
        Destroy(gameObject, 2f);
        yield return new WaitForSeconds(3f);
        LoadManager.LoadScene("Village");
    }

}
