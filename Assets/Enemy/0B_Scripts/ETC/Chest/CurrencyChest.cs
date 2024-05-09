using UnityEngine;

public class CurrencyChest : InteractiveObject
{
    [SerializeField] private DropTableSO _dropTable;
    [SerializeField] private Vector2 _spawnOffset;

    private readonly int _openAnimHash = Animator.StringToHash("Open");

    private Animator _animator;
    private bool isOpened;

    protected override void Awake() {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (isOpened) return;
        base.Update();
    }

    public override void Interaction() {
        _animator.SetTrigger(_openAnimHash);
        isOpened = true;
        DialogueManager.Instance.ExcuseMeUI.SetActive(false);
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
