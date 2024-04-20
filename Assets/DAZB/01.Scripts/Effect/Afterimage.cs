using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : PoolableMono
{
    [SerializeField] private float lifeTime;
    [SerializeField] private Color color;
    private Transform playerTrm;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer sr;

    public override void ResetItem()
    {
        sr = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = PlayerManager.instance.playerSpriteRenderer;
        playerTrm = PlayerManager.instance.playerTrm;
        StartCoroutine(AfterimageRoutine());
    }

    private IEnumerator AfterimageRoutine() {
        sr.sprite = playerSpriteRenderer.sprite;
        transform.position = playerTrm.position;
        sr.color = color;
        yield return new WaitForSeconds(lifeTime);
        PoolManager.Instance.Push(this);
    }
}
