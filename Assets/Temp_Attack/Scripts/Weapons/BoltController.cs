using UnityEngine;

public class BoltController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
    }
}
