using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGearSpinner : MonoBehaviour
{
    [SerializeField] int cogAmount;
    [SerializeField] bool reverse = false;
    float oneTurnAngle;

    float nowZ = 0;
    float goalZ = 0;

    private void Awake() {
        oneTurnAngle = 360f / cogAmount;
    }

    public void Set(float value /* 0 ~ 10 */) {
        goalZ = value * oneTurnAngle * (reverse ? -1 : 1);
        print(goalZ);
    }

    private void Update() {
        Vector3 rotate = transform.localEulerAngles;

        if (Mathf.Abs(nowZ - goalZ) > 0.5f) {
            rotate = Vector3.MoveTowards(new(rotate.x, rotate.y, nowZ), new(rotate.x, rotate.y, goalZ), Time.deltaTime * 50);
            nowZ = rotate.z;

            print($"{nowZ} / {goalZ}");

            transform.localEulerAngles = rotate;
        }
    }
}
