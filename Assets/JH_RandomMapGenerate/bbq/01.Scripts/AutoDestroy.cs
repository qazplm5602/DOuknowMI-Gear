using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        if (TryGetComponent<ParticleSystem>(out ParticleSystem 이기))
        {
            이기.Play();
        }
    }
}
