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
        EnemyBoom boom = PoolManager.Instance.Pop(PoolingType.Boom) as EnemyBoom;
        boom.transform.position = transform.position;
        boom.Init(2, 5);

        PoolManager.Instance.Push(this);
    }
}
