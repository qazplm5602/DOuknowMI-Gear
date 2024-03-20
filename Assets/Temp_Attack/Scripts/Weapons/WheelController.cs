using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private Transform _visualTrm;

    private float _rotationSpeed = 1500f;
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
        _visualTrm.Rotate(0, 0, _rotationSpeed * Time.fixedDeltaTime);
    }

}
