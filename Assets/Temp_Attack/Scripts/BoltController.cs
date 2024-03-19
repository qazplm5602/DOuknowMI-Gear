using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    private void FixedUpdate()
    {
        transform.up *= _speed * Time.fixedDeltaTime;
    }
}
