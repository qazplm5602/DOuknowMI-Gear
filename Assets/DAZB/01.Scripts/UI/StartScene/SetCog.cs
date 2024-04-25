using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCog : MonoBehaviour
{ 

    // cos x sin y
    [SerializeField] private RectTransform center;
    [SerializeField] private float offset;
    [SerializeField] private RectTransform[] cog;
    [SerializeField] private Color topCog;

    private void Start() {
        Setting();
    }

    private void Setting() {
        for (int i = 0; i < cog.Length; ++i) {
            float angle = 360f / cog.Length * i - 30;
            float radians = angle * Mathf.Deg2Rad;
            cog[i].transform.eulerAngles = new Vector3(0, 0, angle - 90);
            cog[i].anchoredPosition = new Vector3(
                Mathf.Cos(radians) * offset,
                Mathf.Sin(radians) * offset
            );
            if (cog[i].transform.rotation.z == 0) {
                cog[i].GetComponent<Image>().color = topCog;
            }
        }
    }
}
