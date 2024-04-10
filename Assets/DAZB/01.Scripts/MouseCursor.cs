using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    private void Update() {
        Cursor.visible = false;
        transform.position = (Vector2)Input.mousePosition;
    }
}
