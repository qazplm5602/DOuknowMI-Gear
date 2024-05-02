using UnityEngine;

public class CurrencyChest : InteractiveObject
{
    [SerializeField] private DropTableSO _dropTable;
    [SerializeField] private Vector2 _spawnOffset;

    private readonly int _openAnimHash = Animator.StringToHash("Open");

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public override void Interaction() {
        _animator.SetTrigger(_openAnimHash);

        Destroy(gameObject, 2f);
    }

    public virtual void DropItems() {
        int randomSmallPartAmount = Random.Range(_dropTable.smallPartAmount.x, _dropTable.smallPartAmount.y + 1);
        int randomBigPartAmount = Random.Range(_dropTable.bigPartAmount.x, _dropTable.bigPartAmount.y + 1);
        for(int i = 0; i < randomSmallPartAmount; ++i) {
            Transform trm = PoolManager.Instance.Pop(PoolingType.SmallPart).transform;
            trm.position = transform.position + (Vector3)_spawnOffset;
        }
        for(int i = 0; i < randomBigPartAmount; ++i) {
            Transform trm = PoolManager.Instance.Pop(PoolingType.BigPart).transform;
            trm.position = transform.position + (Vector3)_spawnOffset;
        }
    }

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + (Vector3)_spawnOffset, 0.2f);
    }
}
