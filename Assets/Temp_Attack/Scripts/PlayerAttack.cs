using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] DeprecatePlayerControlWithAttack control;
    
    private void Awake() {
        control.OnMouseClick += OnChangeMouse;
    }

    void OnChangeMouse(bool isDown) {
        print("OnChangeMouse: "+ isDown.ToString());
    }

    private void Update() {
        Debug.DrawLine(transform.position, GetMouseDir());
    }

    Vector2 GetMouseDir() {
        Vector2 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouseCoords - (Vector2)transform.position;
    }
}
