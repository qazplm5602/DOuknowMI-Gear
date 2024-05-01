using System.Collections;
using UnityEngine;

public class SkillWheel : SkillController
{
    private int directionSign;

    [SerializeField] Transform rotateImageTrm;
    [SerializeField] Rigidbody2D _rigid2d;
    [SerializeField] Vector2 _force = new Vector2(10, 0);

    private void Start()
    {
        directionSign = CheckDirection();
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
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _rigid2d.gravityScale = 0;
            _rigid2d.velocity = Vector2.zero;
            StartCoroutine(MoveRoutine(transform));
        }
        base.OnTriggerEnter2D(collision);
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        _rigid2d.AddForce(_force * directionSign, ForceMode2D.Impulse);
        yield return base.MoveRoutine(startTrm);
    }
}
