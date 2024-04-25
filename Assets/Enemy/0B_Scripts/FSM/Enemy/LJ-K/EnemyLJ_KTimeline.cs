using UnityEngine;
using UnityEngine.Playables;

public class EnemyLJ_KTimeline : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLJ_KPrefab;
    [SerializeField] private Vector2 _spawnPosition;

    private PlayableDirector _playableDirector;

    private void Awake() {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private void Update() {
        if(_playableDirector.time >= 12)
            EndTimeline();
    }

    private void EndTimeline() {
        Instantiate(_enemyLJ_KPrefab, _spawnPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
