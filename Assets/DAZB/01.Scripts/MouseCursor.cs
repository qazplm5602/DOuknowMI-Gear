using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour 
{
    private void Update() {
        if (Cursor.visible == true) Cursor.visible = false;
        transform.position = (Vector2)Input.mousePosition;
    }
}
