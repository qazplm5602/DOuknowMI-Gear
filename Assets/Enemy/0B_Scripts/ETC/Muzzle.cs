using System.Collections;
using UnityEngine;

public class Muzzle : PoolableMono
{
    [SerializeField] private float _lifeTime;

    [SerializeField] private Sprite[] _muzzleSprites;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int direction) {
        _spriteRenderer.sprite = _muzzleSprites[Random.Range(0, 4)];

        _spriteRenderer.flipX = direction == 1;
        
        StartCoroutine(Timer());
    }

    private IEnumerator Timer() {
        yield return new WaitForSeconds(_lifeTime);

        PoolManager.Instance.Push(this);
    }

    public override void ResetItem() {
        
    }
}
