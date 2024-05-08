using UnityEngine;

public enum PartSize {
    Small,
    Big
}

public class Part : PoolableMono
{
    public Transform playerTrm;

    [SerializeField] private PartSize _partSize;
    [SerializeField] private float _magnetSpeed;
    [SerializeField] private Sprite[] _sprites;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        float angle = Random.Range(-60f, 60f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        _rigidbody.AddForce(direction.normalized * 5f, ForceMode2D.Impulse);
    }

    private void Update() {
        if(playerTrm != null) {
            Vector3 direction = playerTrm.position - transform.position;
            transform.position += direction.normalized * Time.deltaTime * _magnetSpeed;

            if(direction.magnitude < 0.05f) {
                GetPartAmount();
                PlayerManager.instance.playerPart.IncreasePart(GetPartAmount());
                PoolManager.Instance.Push(this);
            }
        }
    }

    private int GetPartAmount() {
        switch (_partSize) {
            case PartSize.Small:
                return Random.Range(10, 51);
            case PartSize.Big:
                return Random.Range(150, 301);
        }

        Debug.LogError("[Part] PartSize not found");
        return 0;
    }

    public override void ResetItem() {
        playerTrm = null;
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
        gameObject.SetActive(true);
    }
}
