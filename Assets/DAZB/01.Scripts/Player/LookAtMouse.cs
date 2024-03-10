using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private void Update() {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(mouse.x - transform.position.x) < 0.1f) {
            return;
        }
        transform.eulerAngles = mouse.x > transform.position.x ? Vector2.zero : new Vector2(0, 180);
    }
}
