using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using System;

public class EnemyLJ_KTimeline : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLJ_KPrefab;
    [SerializeField] private Transform _spawnTransform;

    private PlayableDirector _playableDirector;

    private void Awake() {
        _playableDirector = GetComponent<PlayableDirector>();
        
        TimelineAsset ta = _playableDirector.playableAsset as TimelineAsset;
        IEnumerable<TrackAsset> temp = ta.GetOutputTracks();

        foreach(var k in temp) {
            if(k is CinemachineTrack) {
                _playableDirector.SetGenericBinding(k, Camera.main.GetComponent<CinemachineBrain>());
            }
        }

        _playableDirector.stopped += EndTimeline;
    }

    private void EndTimeline(PlayableDirector director) {
        Instantiate(_enemyLJ_KPrefab, _spawnTransform.position, Quaternion.identity).GetComponent<EnemyLJ_K>().bossStage = transform.parent.GetComponent<BossStage>();
        Destroy(gameObject);
    }
}
