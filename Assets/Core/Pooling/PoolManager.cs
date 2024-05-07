using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    public Canvas _poolingCanvas;
    [SerializeField] private PoolingListSO _poolingList;

    private Dictionary<PoolingType, Pool<PoolableMono>> _pools = new Dictionary<PoolingType, Pool<PoolableMono>>();

    private void Awake() {
        foreach(PoolingPair pair in _poolingList.list) {
            CreatePool(pair.prefab, pair.type, pair.count);
        }
    }

    public void CreatePool(PoolableMono prefab, PoolingType poolingType, int count = 10) {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, poolingType, transform, count);
        _pools.Add(poolingType, pool);
    }

    public PoolableMono Pop(PoolingType type, bool usePosition = false, Transform spawnTrm = null) {
        if(_pools.ContainsKey(type) == false) {
            Debug.LogError($"Prefab does not Exist on Pool : {type}");
            return null;
        }

        PoolableMono item = _pools[type].Pop();
        item.ResetItem();
        if(usePosition)
        {
            item.transform.position = spawnTrm.position;
        }
        return item;
    }

    public void Push(PoolableMono obj, bool resetParent = false) {
        if(resetParent)
            obj.transform.parent = transform;
        _pools[obj.poolingType].Push(obj);
    }
}
