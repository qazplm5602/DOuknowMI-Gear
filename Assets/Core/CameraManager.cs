using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera v_cam;
    private CinemachineBasicMultiChannelPerlin v_perlin;
    bool disabled = false;

    private void Awake() {
        LoadConfig();
        v_perlin = v_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float amplitude, float frequency, float time) {
        if (disabled) return;
        
        v_perlin.m_AmplitudeGain = amplitude;
        v_perlin.m_FrequencyGain = frequency;
        StartCoroutine(ShakeRoutine(time));
    }

    private IEnumerator ShakeRoutine(float time) {
        yield return new WaitForSeconds(time);
        v_perlin.m_AmplitudeGain = 0;
        v_perlin.m_FrequencyGain = 0;
    }

    public void LoadConfig() {
        disabled = PlayerPrefs.GetInt("generic.camerashake", 1) == 0;
    }
}
