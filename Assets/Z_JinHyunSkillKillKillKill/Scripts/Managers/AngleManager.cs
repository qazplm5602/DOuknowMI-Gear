using System.Collections;
using UnityEngine;

public class AngleManager : MonoBehaviour
{
    public static Quaternion GetTargetDirection(Vector3 playerPos, Vector3 mousePos)
    {
        Vector3 pPos = playerPos;
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mPos.y - pPos.y, mPos.x - pPos.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        return lookRotation;
    }
}