using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SkillWheel : SkillController
{
    private int directionSign;

    [SerializeField] Transform rotateImageTrm;
    [SerializeField] Rigidbody2D _rigid2d;
    [SerializeField] Vector2 _force = new Vector2(10, 0);
    

    Vector2 _lastVelocity;
    private int _layerMaskValue;

    private void Start()
    {
        directionSign = CheckDirection();
        _layerMaskValue = (int)Mathf.Log(_groundLayerMask.value, 2);
    }

    public int CheckDirection()
    {
        float rotationZ = transform.localEulerAngles.z;

        if (rotationZ > 90f && rotationZ < 270f) return -1; 
        else return 1; // 오른쪽
    }

    private void Update()
    {
        rotateImageTrm.transform.Rotate(0, 0, 160 * Time.deltaTime);
        if (Physics2D.Raycast(transform.position, Vector2.down,0.35f, _groundLayerMask))
        {
            KillRigidByBool();
            StartCoroutine(MoveRoutine(transform));
        }
        else
        {
            KillRigidByBool(false);
        }

        if(Physics2D.Raycast(transform.position, Vector2.right, 0.7f, _wallLayerMask)
        || Physics2D.Raycast(transform.position, Vector2.left, 0.7f, _wallLayerMask))
        {
            Destroy(gameObject);
        }
    }

    private void KillRigidByBool(bool kill = true)
    {
        _lastVelocity = kill == true ? Vector2.zero : _rigid2d.velocity;
        _rigid2d.gravityScale = kill == true ? 0: 1;
        _rigid2d.velocity = kill == true ? Vector2.zero : _lastVelocity;
        print(_rigid2d.velocity);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable target))
        {
            PoolManager.Instance.Pop(PoolingType.Effect_Impact, true, collision.transform);
            target.ApplyDamage(Mathf.FloorToInt(_damage), PlayerManager.instance.playerTrm);
            if (_canPierce)
            {
                if (_pierceCount <= 0) Destroy(gameObject);
                --_pierceCount;
            }
        }
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        _rigid2d.AddForce(_force * directionSign, ForceMode2D.Impulse);
        yield return base.MoveRoutine(startTrm);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-0.7f, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-0.7f, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0.7f, 0, 0));
    }
}
