using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip[] _audios;
    
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _bgmMixerGroup;

    private Dictionary<string, AudioClip> _audioClipDictionary;
    private AudioSource _audioSource;
    private AudioSource _bgmPlayer;

    private void Awake() {
        _audioClipDictionary = new Dictionary<string, AudioClip>();
        _audioSource = GetComponent<AudioSource>();

        foreach(AudioClip clip in _audios) {
            _audioClipDictionary.Add(clip.name, clip);
        }
        
        SetVolume("Master", PlayerPrefs.GetInt("sound.master", 100));
        SetVolume("SFX", PlayerPrefs.GetInt("sound.effect", 100));
        SetVolume("BGM", PlayerPrefs.GetInt("sound.bgm", 100));
    }

    public void SetVolume(string name, int value /* 0 ~ 100 */) {
        // print($"{name} {-((100 - value) * 0.8f)}");
        _audioMixer.SetFloat(name, -((100 - value) * 0.8f));
    }

    public void PlaySound(string clipName) {
        if(!_audioClipDictionary.ContainsKey(clipName)) {
            Debug.LogError($"[SoundManager] {clipName} Clip Not Found");
            return;
        }

        _audioSource.PlayOneShot(_audioClipDictionary[clipName]);
    }

    private void PlayBGM(string clipName) {
        if(!_audioClipDictionary.ContainsKey(clipName)) {
            Debug.LogError($"[SoundManager] {clipName} Clip Not Found");
            return;
        }

        if(_bgmPlayer) Destroy(_bgmPlayer.gameObject);

        GameObject obj = new GameObject(clipName);
        obj.transform.SetParent(transform);
        _bgmPlayer = obj.AddComponent<AudioSource>();
        _bgmPlayer.outputAudioMixerGroup = _bgmMixerGroup;
        _bgmPlayer.loop = true;
        _bgmPlayer.clip = _audioClipDictionary[clipName];
        _bgmPlayer.Play();
    }
}
