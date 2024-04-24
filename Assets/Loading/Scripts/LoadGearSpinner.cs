using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGearSpinner : MonoBehaviour
{
    [SerializeField] int cogAmount;
    [SerializeField] bool reverse = false;
    float firstAngle;

    private void Awake() {
        firstAngle = transform.eulerAngles.z;
    }

    IEnumerator process;

    public void Set(float value /* 0 ~ 1 */) {
        if (process != null)
            StopCoroutine(process);

        process = SmoothSpinGear(value * (reverse ? -1 : 1));
        StartCoroutine(process);
    }

    IEnumerator SmoothSpinGear(float loaded) {
        float time = 0;
        Vector3 rotate = transform.localEulerAngles;
        
        while (time < 1) {
            yield return null;
            time += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(rotate, new Vector3(rotate.x, rotate.y, (360f * loaded) + firstAngle), time);
        }

        process = null;
    }
}
