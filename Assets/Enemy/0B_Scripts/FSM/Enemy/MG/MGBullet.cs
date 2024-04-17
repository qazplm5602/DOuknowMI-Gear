using UnityEngine;

public class MGBullet : EnemyProjectile
{
    protected override void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        if(((1 << other.gameObject.layer) & _whatIsEnemy) > 0) {
            Boom();
        }
    }

    private void Boom() {
        PoolManager.Instance.Pop(PoolingType.Boom).transform.position = transform.position;

        PoolManager.Instance.Push(this);
    }
}
