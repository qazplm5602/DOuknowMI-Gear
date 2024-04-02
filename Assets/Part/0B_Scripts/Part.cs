using UnityEngine;

public enum PartSize {
    Small,
    Big
}

[RequireComponent(typeof(Rigidbody2D))]
public class Part : PoolableMono
{
    [SerializeField] private PartSize _partSize;

    [Header("Magnet Settings")]
    [SerializeField] private float _magnetRadius;
    [SerializeField] private float _magnetSpeed;
    [SerializeField] private LayerMask _whatIsPlayer;

    [Header("Ground Check Settings")]
    [SerializeField] private Vector2 _groundCheckBox;
    [SerializeField] private Vector2 _groundCheckOffset;
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody2D _rigidbody;
    private Transform _playerTrm;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        float angle = Random.Range(-60f, 60f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        _rigidbody.AddForce(direction.normalized * 5f, ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        if(Physics2D.OverlapBox((Vector2)transform.position + _groundCheckOffset, _groundCheckBox, 0, _whatIsGround)) {
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = Vector2.zero;
        }
        else _rigidbody.gravityScale = 1f;
    }

    private void Update() {
        if(_playerTrm) {
            Vector3 direction = _playerTrm.position - transform.position;
            transform.position += direction.normalized * Time.deltaTime * _magnetSpeed;
        }
        else {
            if(Physics2D.OverlapCircle(transform.position, _magnetRadius, _whatIsPlayer)) {
                _playerTrm = PlayerManager.instance.playerTrm;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out PlayerPart playerPart)) {
            playerPart.IncreasePart(GetPartAmount());
            gameObject.SetActive(false);
            PoolManager.Instance.Push(this);
        }
    }

    private uint GetPartAmount() {
        switch (_partSize) {
            case PartSize.Small:
                return (uint)Random.Range(10, 51);
            case PartSize.Big:
                return (uint)Random.Range(150, 301);
        }

        Debug.LogError("[Part] PartSize not found");
        return 0;
    }

    public override void ResetItem() {
        gameObject.SetActive(true);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + _groundCheckOffset, _groundCheckBox);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _magnetRadius);
    }
}
