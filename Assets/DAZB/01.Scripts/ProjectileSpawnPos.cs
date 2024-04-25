using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnPos : MonoBehaviour
{
    // cos x sin y
    [SerializeField] private float offset;
    [SerializeField] private Transform playerTrm;

    private void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        float angle = Mathf.Atan2(mousePos.y - playerTrm.position.y, mousePos.x -  playerTrm.position.x) * Mathf.Rad2Deg;
        transform.position = (Vector2)playerTrm.position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * offset,Mathf.Sin(angle * Mathf.Deg2Rad) * offset);
    }
}
