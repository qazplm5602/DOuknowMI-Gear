using Cinemachine;
using System.Collections;
using UnityEngine;

public class AngleManager : MonoBehaviour
{
    public static Camera orthographicCam = Camera.main.transform.GetChild(0).GetComponent<Camera>();
    public static Quaternion GetTargetDirection(Vector3 playerPos, Vector3 mousePos)
    {
        if (orthographicCam == null) print("Maincamera's child require orthograpic camera");
        Vector3 pPos = playerPos;
        Vector3 mPos = orthographicCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mPos.y - pPos.y, mPos.x - pPos.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        return lookRotation;
    }
}