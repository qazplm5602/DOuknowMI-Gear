using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] DeprecatePlayerControlWithAttack control;
    [SerializeField] GearManager gearManager;

    bool mouseDisable = false;

    private void Awake() {
        control.OnMouseClick += OnChangeMouse;
    }
    private void OnDestroy() {
        control.OnMouseClick -= OnChangeMouse;
    }

    void OnChangeMouse(bool isDown) {
        print("OnChangeMouse: "+ isDown.ToString());
        if (isDown && !mouseDisable) {
            mouseDisable = true;

            // 발사 후 돌림
            gearManager.GetGearResult();

            gearManager.StartRoll(GearRollFinish);
        }
    }

    private void Update() {
        Debug.DrawLine(transform.position, GetMouseDir());
    }

    Vector2 GetMouseDir() {
        Vector2 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouseCoords - (Vector2)transform.position;
    }

    void GearRollFinish() {
        print("GearRollFinish");
        mouseDisable = false;
    }
}
