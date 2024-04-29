using UnityEngine;
using UnityEngine.Playables;

public class EnemyLJ_KTimeline : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLJ_KPrefab;
    [SerializeField] private Transform _spawnTransform;

    private PlayableDirector _playableDirector;

    private void Awake() {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private void Update() {
        if(_playableDirector.time >= 12)
            EndTimeline();
    }

    private void EndTimeline() {
        Instantiate(_enemyLJ_KPrefab, _spawnTransform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
