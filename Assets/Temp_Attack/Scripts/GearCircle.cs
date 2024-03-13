using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCircle : MonoBehaviour
{
    RectTransform _transform;
    [SerializeField] GameObject gearCogPrefab;
    public GearSO gearSO;

    // GameObject cog;
    
    private void Awake() {
        _transform = GetComponent<RectTransform>();
    }

    private void Start() {
        // cog = Instantiate(gearCogPrefab, transform);
    }

    private void Update() {
        // cog.transform.RotateAround(transform.position, new Vector3(0,0,1) , Time.deltaTime * 100);
    }

    public void Init() {
        for (int i = 0; i < gearSO.CogAmount; ++i) {
            var cog = Instantiate(gearCogPrefab, transform);
            cog.transform.RotateAround(transform.position, new Vector3(0,0,1), (360f / gearSO.CogAmount) * i);
        }
    }
}
