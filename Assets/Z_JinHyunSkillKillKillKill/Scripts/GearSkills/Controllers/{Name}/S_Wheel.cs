using System.Collections;
using System.Collections.Generic;
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

        print(transform.localEulerAngles.z);

        if (rotationZ > 90f && rotationZ < 270f) return -1; 
        else return 1; // 오른쪽
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(directionSign);
        StartCoroutine(MoveRoutine(transform));
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        if (_destroyByTime)
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
            yield break;
        }
        Vector3 firstPos = startTrm.position;
        bool notMaxDistance = IsInRange(firstPos, currentPos: transform.position);
        while (notMaxDistance)
        {
            notMaxDistance = IsInRange(firstPos, currentPos: transform.position);

            transform.position += _moveSpeed * Time.deltaTime * startTrm.right;
            if (isDamageCasting && _attackTriggerCalled) DamageCasting();

            yield return null;
        }
        if (isDamageCasting) DamageCasting();
        Destroy(gameObject);
        yield break;
    }
}
