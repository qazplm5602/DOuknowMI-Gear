using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCircle : MonoBehaviour
{
    RectTransform _transform;
    [SerializeField] GameObject gearCogPrefab;

    GameObject cog;
    
    private void Awake() {
        _transform = GetComponent<RectTransform>();
    }

    private void Start() {
        cog = Instantiate(gearCogPrefab, transform);
    }

    private void Update() {
        cog.transform.RotateAround(transform.position, new Vector3(0,0,1) , Time.deltaTime * 100);
    }
}
