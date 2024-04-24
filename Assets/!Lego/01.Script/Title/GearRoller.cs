using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GearRoller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float rotationSpeed = 50f;

    private bool isPressed = false;

    private float rotateValue = 0;

    void Update()
    {
        if (isPressed)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            rotateValue += Mathf.Abs(rotationSpeed * Time.deltaTime);
            if (rotateValue > 360)
            {
                TitleManager.Instance.ChangeSceneToVillage();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        rotateValue = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
