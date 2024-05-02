using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private float _collectRadius = 3f;
    [SerializeField] private LayerMask _whatIsItem;
    private Collider2D[] _colliders;

    private void Awake() {
        _colliders =  new Collider2D[6];
    }

    private void Update() {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _collectRadius, _colliders, _whatIsItem);

        if(count > 0) {
            for(int i = 0; i < count; ++i) {
                _colliders[i].GetComponent<Part>().playerTrm = transform;
            }
        }
    }
}
