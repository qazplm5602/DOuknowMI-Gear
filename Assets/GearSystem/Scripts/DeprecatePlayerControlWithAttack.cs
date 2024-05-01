// 이 스크립트는 PlayerAttack.cs를 구현하기 위해 만들어진 컨트롤 로직임
// PlayerAttack 외에 참조하여 사용 ㄴㄴ
// 여기에서 다른 기능을 구현 (jump, move, ..etc) 하지마셈
// Player Control 베이스가 생기면 삭제될 예정
// 2024 - 03 - 09

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete] // Deprecated Class
public class DeprecatePlayerControlWithAttack : MonoBehaviour
{
    public event Action<bool> OnMouseClick; // 마우스 이벤트 UP / DOWN 이 변경될때 실행함
    bool isDown = false;

    private void Update() {
        if (!isDown && Input.GetMouseButtonDown(0)) {
            isDown = true;
            OnMouseClick?.Invoke(true); // Down 이벤트
        } else if (isDown && Input.GetMouseButtonUp(0)) {
            isDown = false;
            OnMouseClick?.Invoke(false); // Up 이벤트
        }
    }
}
