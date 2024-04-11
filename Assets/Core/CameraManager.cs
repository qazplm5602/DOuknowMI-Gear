using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera v_cam;
    private CinemachineBasicMultiChannelPerlin v_perlin;

    private void Awake() {
        v_perlin = v_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float amplitude, float frequency, float time) {
        v_perlin.m_AmplitudeGain = amplitude;
        v_perlin.m_FrequencyGain = frequency;
        StartCoroutine(ShakeRoutine(time));
    }

    private IEnumerator ShakeRoutine(float time) {
        yield return new WaitForSeconds(time);
        v_perlin.m_AmplitudeGain = 0;
        v_perlin.m_FrequencyGain = 0;
    }
}
