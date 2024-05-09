using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera v_cam;
    [SerializeField] private Volume globalVolume;
    private CinemachineBasicMultiChannelPerlin v_perlin;
    private Vignette vg;

    private void Awake() {
        v_perlin = v_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        globalVolume.profile.TryGet(out vg);
    }

    public void ShakeCamera(float amplitude, float frequency, float time) {
        if (!GameManager.Instance.cameraShake) return;

        v_perlin.m_AmplitudeGain = amplitude;
        v_perlin.m_FrequencyGain = frequency;
        StartCoroutine(ShakeRoutine(time));
    } 

    public void BloodScreen(float value) {
        StartCoroutine(BloodScreenRoutine(value));
    }

    private IEnumerator BloodScreenRoutine(float value) {
        value = Mathf.Clamp(value, 0.3f, 1);
        vg.intensity.value = value;
        DOTween.To(
            x => vg.intensity.value = x, value, 0, 0.35f
        );
        //vg.intensity.value = 0;
        yield return null;
    }

    private IEnumerator ShakeRoutine(float time) {
        yield return new WaitForSeconds(time);
        v_perlin.m_AmplitudeGain = 0;
        v_perlin.m_FrequencyGain = 0;
    }
}
