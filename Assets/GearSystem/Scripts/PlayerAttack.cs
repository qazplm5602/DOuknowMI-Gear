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
        if (isDown && !mouseDisable) {
            mouseDisable = true;

            // 발사 후 돌림
            GearRunModule();

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
        mouseDisable = false;
    }

    // 기어 공격 실행
    void GearRunModule() {
        var gearList = gearManager.GetGearResult();
        
        foreach (var item in gearList)
        {
            // print("-----------------");
            // print(item.type.ToString());
            // print(item.script);
            // print(String.Join(',', item.gearIdx));

            item.script.Use();
        }
    }
}