using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool _isDestroyed = false;
    
    public static T Instance {
        get {
            if(_isDestroyed) _instance = null;

            if(_instance == null) {
                _instance = FindObjectOfType<T>();

                if(_instance == null) {
                    Debug.LogError($"{typeof(T).Name} Singleton is not exist!");
                }
            }

            return _instance;
        }
    }

    private void OnDestroy() {
        _isDestroyed = true;
    }
}
