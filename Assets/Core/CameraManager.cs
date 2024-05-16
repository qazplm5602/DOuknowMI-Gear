using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System.Net.WebSockets;

public class CameraManager : MonoSingleton<CameraManager>
{
    public CinemachineVirtualCamera v_cam;
    public Volume globalVolume;
    private CinemachineBasicMultiChannelPerlin v_perlin;
    private Vignette vg;
    private DepthOfField depth;
    private ChromaticAberration chromaticAberration;

    private void Start() {
        v_perlin = v_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        globalVolume.profile.TryGet(out vg);
        globalVolume.profile.TryGet(out depth);
        globalVolume.profile.TryGet(out chromaticAberration);
        PlayerManager.instance.player.HealthCompo.OnDead += DeadEvent;
    }

    public void ShakeCamera(float amplitude, float frequency, float time) {
        if (!GameManager.Instance.cameraShake) return;

        v_perlin.m_AmplitudeGain = amplitude;
        v_perlin.m_FrequencyGain = frequency;
        StartCoroutine(ShakeRoutine(time));
    } 

    public void SetChromaticAberration(int currentHp, int maxHp) {
        float healthPercentage = (float)currentHp / maxHp;
        float intensityValue = Mathf.Lerp(0.7f, 0f, Mathf.InverseLerp(0.05f, 0.4f, healthPercentage));
        chromaticAberration.intensity.value = intensityValue;
    }

    public void ResetChromaticAberration() {
        chromaticAberration.intensity.value = 0;
    }

    public void HitMethod(float value) {
        StartCoroutine(HurtScreen(value));
    }

    private void DeadEvent() {
        depth.focusDistance.value = 0.5f;
        PlayerManager.instance.player.HealthCompo.OnDead -= DeadEvent;
    }

//0~15 데미지를 15로 나눠서 보냄, 그러면 * 4를 하면? 0~4
    private IEnumerator HurtScreen(float value) {
        float vignetteValue = Mathf.Clamp(value, 0.3f, 1);
        float depthValue = 4 - Mathf.Clamp(Mathf.Lerp(0, 4, value * 4), 2, 4);
        vg.intensity.value = vignetteValue;
        DOTween.To(
            x =>  vg.intensity.value = x, vignetteValue, 0, 0.35f
        );
        depth.focusDistance.value = depthValue;
        DOTween.To(
            x => depth.focusDistance.value = x, depthValue, 4, 0.5f
        ).SetEase(Ease.InQuad);
        //vg.intensity.value = 0;
        yield return null;
    }

    private IEnumerator ShakeRoutine(float time) {
        yield return new WaitForSeconds(time);
        v_cam.transform.localPosition = new Vector3(0, 0, -0);
        v_perlin.m_AmplitudeGain = 0;
        v_perlin.m_FrequencyGain = 0;
    }
}
